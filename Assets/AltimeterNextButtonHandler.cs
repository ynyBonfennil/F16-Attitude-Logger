using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using System;

public class AltimeterNextButtonHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Text frameNumberText;

    public int playbackLatency = 10;
    private int holdCounter;
    private bool isHold;
    private string filepath;

    void Start()
    {
        holdCounter = 0;
        isHold = false;

        DateTime now = DateTime.Now;
        filepath = "Assets/Log/Altitude/"
            + now.Year.ToString() + "-"
            + now.Month.ToString() + "-"
            + now.Day.ToString() + "-"
            + now.Hour.ToString() + "-"
            + now.Minute.ToString() + "-"
            + now.Second.ToString() + ".log";
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine("frame,altitude");
        writer.Close();
    }

    void Update()
    {
        if (isHold)
        {
            holdCounter++;
            if (holdCounter > playbackLatency)
            {
                WriteAltitudeData();
                NextFrame();
                holdCounter = 0;
            }
        }
    }

    public void OnNextButtonDown()
    {
        isHold = true;
    }

    public void OnNextButtonUp()
    {
        isHold = false;
    }

    private void WriteAltitudeData()
    {
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine(
            videoPlayer.frame.ToString()
            + ","
        );
        Debug.Log("Frame #" + videoPlayer.frame.ToString() + " Done");
        writer.Close();

    }

    private void NextFrame()
    {
        long nextFrame = videoPlayer.frame + 1;
        videoPlayer.frame = nextFrame;
        videoPlayer.Play();

        // It seems videoPlayer.Play() is non-blocking function
        // hense when you call videoPlayer.frame here, the frame
        // number might not be updated.
        frameNumberText.text = ("Frame #" + nextFrame.ToString());
    }
}
