using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private int keyHoldFrames = 0;
    private const int KEY_HOLD_DELAY_FRAMES = 400;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.playbackSpeed = 0;
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            keyHoldFrames = 0;
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (keyHoldFrames == 0 || keyHoldFrames > KEY_HOLD_DELAY_FRAMES)
                {
                    keyHoldFrames++;
                    videoPlayer.frame++;
                    videoPlayer.Play();
                }
                else
                {
                    keyHoldFrames++;
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (keyHoldFrames == 0 || keyHoldFrames > KEY_HOLD_DELAY_FRAMES)
                {
                    keyHoldFrames++;
                    videoPlayer.frame--;
                    videoPlayer.Play();
                }
                else
                {
                    keyHoldFrames++;
                }
            }
        }
    }
}
