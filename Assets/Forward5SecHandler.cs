using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Forward5SecHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject alignmentDoneIcon;
    public Text frameNumberText;

    public void Next5Seconds()
    {
        alignmentDoneIcon.SetActive(false);

        long nextFrame = videoPlayer.frame + 30*5;
        videoPlayer.frame = nextFrame;
        videoPlayer.Play();

        // It seems videoPlayer.Play() is non-blocking function
        // hense when you call videoPlayer.frame here, the frame
        // number might not be updated.
        frameNumberText.text = ("Frame #" + nextFrame.ToString());
    }
}
