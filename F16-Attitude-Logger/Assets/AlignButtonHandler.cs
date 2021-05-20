using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;
using System;

public class AlignButtonHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject pilotView;
    public GameObject alignmentDoneIcon;
    private string filepath;

    // Start is called before the first frame update
    void Start()
    {
        DateTime now = DateTime.Now;
        filepath = "Assets/Log/Attitude/"
            + now.Year.ToString() + "-"
            + now.Month.ToString() + "-"
            + now.Day.ToString() + "-"
            + now.Hour.ToString() + "-"
            + now.Minute.ToString() + "-"
            + now.Second.ToString() + ".log";
        StreamWriter writer = new StreamWriter(filepath, true);
        writer.WriteLine("frame,quat_w,quat_x,quat_y,quat_z,euler_x,euler_y,euler_z");
        writer.Close();
    }

    public void WriteAttitudeData()
    {
        Quaternion attitude = pilotView.transform.rotation;
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

    public void ShowDoneMark()
    {
        alignmentDoneIcon.SetActive(true);
    }
}
