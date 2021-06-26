using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using System;


public class AirspeedNextButtonHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Text frameNumberText;
    public GameObject indicatorRig;
    private string filepath;
    private bool isPointerDown = false;
    private int holdTime = 0;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
 
        DateTime now = DateTime.Now;
        filepath = "Assets/csv/Airspeed/"
            + now.Year.ToString() + "-"
            + now.Month.ToString() + "-"
            + now.Day.ToString() + "-"
            + now.Hour.ToString() + "-"
            + now.Minute.ToString() + "-"
            + now.Second.ToString() + ".csv";
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine("frame,airspeed");
        writer.Close();
    }

    void Update()
    {
        if (isPointerDown)
        {
            holdTime += 1;
            if (holdTime > 10000)
            {
                holdTime -= 5000;
            }
        }
        else {
            holdTime = 0;
        }

        if (holdTime == 1)
        {
            NextFrame();
        }
        else if (holdTime >= 30 && holdTime%2 == 0)
        {
            NextFrame();
        }
    }

    public void OnPointerDown()
    {
        isPointerDown = true;
    }

    public void OnPointerUp()
    {
        isPointerDown = false;
    }

    private void NextFrame()
    {
        WriteAirspeedData();
        long nextFrame = videoPlayer.frame + 1;
        videoPlayer.frame = nextFrame;
        videoPlayer.Play();

        // It seems videoPlayer.Play() is non-blocking function
        // hense when you call videoPlayer.frame here, the frame
        // number might not be updated.
        frameNumberText.text = ("Frame #" + nextFrame.ToString());
    }

    private void WriteAirspeedData()
    {
        float airspeed = - rectTransform.anchoredPosition.y * 0.07941259f - 8.13899635f;
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine(
            videoPlayer.frame.ToString()
            + "," + airspeed.ToString()
        );
        Debug.Log("Frame #" + videoPlayer.frame.ToString() + " Done");
        writer.Close();
    }
}
