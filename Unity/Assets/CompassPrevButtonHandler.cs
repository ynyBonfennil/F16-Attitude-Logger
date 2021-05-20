using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CompassPrevButtonHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Text frameNumberText;

    public int playbackLatency = 10;
    private int holdCounter;
    private bool isHold;

    void Start()
    {
        holdCounter = 0;
        isHold = false;
    }

    void Update()
    {
        if (isHold)
        {
            holdCounter++;
            if (holdCounter > playbackLatency)
            {
                PreviousFrame();
                holdCounter = 0;
            }
        }
    }

    public void OnPrevButtonDown()
    {
        isHold = true;
    }

    public void OnPrevButtonUp()
    {
        isHold = false;
    }

    private void PreviousFrame()
    {
        long nextFrame = videoPlayer.frame - 1;
        videoPlayer.frame = nextFrame;
        videoPlayer.Play();

        // It seems videoPlayer.Play() is non-blocking function
        // hense when you call videoPlayer.frame here, the frame
        // number might not be updated.
        frameNumberText.text = ("Frame #" + nextFrame.ToString());
    }
}
