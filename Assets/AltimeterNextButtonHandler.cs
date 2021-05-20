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
    public int startFrame;
    public GameObject altimeter;
    private int holdCounter;
    private bool isHold;
    private string filepath;
    private RectTransform altimeterRectTransform;

    void Start()
    {
        holdCounter = 0;
        isHold = false;
        altimeterRectTransform = altimeter.GetComponent<RectTransform>();

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

        videoPlayer.frame = startFrame;
        videoPlayer.Play();
        frameNumberText.text = ("Frame #2000");
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
            + (-0.008526*altimeterRectTransform.anchoredPosition.y-8.3581).ToString()
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
