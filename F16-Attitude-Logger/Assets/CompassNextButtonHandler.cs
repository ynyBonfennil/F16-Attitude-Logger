using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using System;

public class CompassNextButtonHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Text frameNumberText;
    public GameObject compassRig;
    private string filepath;
    private bool isPointerDown = false;
    private int holdTime = 0;

    void Start()
    {
        DateTime now = DateTime.Now;
        filepath = "Assets/csv/Direction/"
            + now.Year.ToString() + "-"
            + now.Month.ToString() + "-"
            + now.Day.ToString() + "-"
            + now.Hour.ToString() + "-"
            + now.Minute.ToString() + "-"
            + now.Second.ToString() + ".csv";
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine("frame,direction");
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
        else{
            holdTime = 0;
        }

        if (holdTime == 1)
        {
            NextFrame();
        }
        else if (holdTime >= 30 && holdTime%10 == 0)
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
        WriteCompassData();
        long nextFrame = videoPlayer.frame + 1;
        videoPlayer.frame = nextFrame;
        videoPlayer.Play();

        // It seems videoPlayer.Play() is non-blocking function
        // hense when you call videoPlayer.frame here, the frame
        // number might not be updated.
        frameNumberText.text = ("Frame #" + nextFrame.ToString());
    }
    private void WriteCompassData()
    {
        float direction = - compassRig.GetComponent<RectTransform>().anchoredPosition.x / 94.4f;
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine(
            videoPlayer.frame.ToString()
            + "," + direction.ToString()
        );
        Debug.Log("Frame #" + videoPlayer.frame.ToString() + " Done");
        writer.Close();
    }
}