using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Preferences : MonoBehaviour
{
    public int targetFrameRate = 30;
    public int videoStartFrame = 0;
    public VideoPlayer videoPlayer;
    public Text frameNumberText;
    void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }

    void Start()
    {
        videoPlayer.frame = videoStartFrame;
        frameNumberText.text = ("Frame #" + videoStartFrame.ToString());
        videoPlayer.Play();
    }
}
