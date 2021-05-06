using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class NextButtonHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Text alignmentDoneText;
    public Text frameNumberText;
    public void NextFrame()
    {
        alignmentDoneText.text = "";

        long nextFrame = videoPlayer.frame + 1;
        videoPlayer.frame = nextFrame;
        videoPlayer.Play();

        // It seems videoPlayer.Play() is non-blocking function
        // hense when you call videoPlayer.frame here, the frame
        // number might not be updated.
        frameNumberText.text = ("Frame #" + nextFrame.ToString());
    }
}
