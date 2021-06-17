using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using System;

public class NextButtonHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // public GameObject alignmentDoneIcon;
    public Text frameNumberText;
    public int videoStartFrame = 0;
    public GameObject cameraRig;
    private string filepath;
    private bool isPointerDown = false;
    private int holdTime = 0;

    void Start()
    {
        // Create File
        DateTime now = DateTime.Now;
        filepath = "Assets/csv/Attitude/"
            + now.Year.ToString() + "-"
            + now.Month.ToString() + "-"
            + now.Day.ToString() + "-"
            + now.Hour.ToString() + "-"
            + now.Minute.ToString() + "-"
            + now.Second.ToString() + ".csv";
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine("frame,w,x,y,z,euler_x,euler_y,euler_z");
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
        else
        {
            holdTime = 0;
        }
        
        if (holdTime == 1)
        {
            NextFrame();
        }
        else if (holdTime >= 30 && holdTime%5 == 0)
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
        WriteAttitudeData();

        // alignmentDoneIcon.SetActive(false);

        long nextFrame = videoPlayer.frame + 1;
        videoPlayer.frame = nextFrame;
        videoPlayer.Play();

        // It seems videoPlayer.Play() is non-blocking function
        // hense when you call videoPlayer.frame here, the frame
        // number might not be updated.
        frameNumberText.text = ("Frame #" + nextFrame.ToString());
    }
    private void WriteAttitudeData()
    {
        // Quaternion attitude = cameraRig.GetComponent<CameraController>().gyroValue;
        Quaternion attitude = cameraRig.transform.localRotation;
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine(
            videoPlayer.frame.ToString()
            + "," + attitude.w.ToString()
            + "," + attitude.x.ToString()
            + "," + attitude.y.ToString()
            + "," + attitude.z.ToString()
            + "," + attitude.eulerAngles.x.ToString()
            + "," + attitude.eulerAngles.y.ToString()
            + "," + attitude.eulerAngles.z.ToString()
        );
        Debug.Log("Frame #" + videoPlayer.frame.ToString() + " Done");
        writer.Close();
    }
}
