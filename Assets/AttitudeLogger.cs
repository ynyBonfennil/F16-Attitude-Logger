using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using System;

public class AttitudeLogger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject pilotView;
    private string filepath;

    void Start()
    {
        DateTime now = DateTime.Now;
        filepath = "Assets/Log/"
            + now.Year.ToString() + "-"
            + now.Month.ToString() + "-"
            + now.Day.ToString() + "-"
            + now.Minute.ToString() + "-"
            + now.Second.ToString() + ".log";

        WriteData("frame,rot_x,rot_y,rot_z", filepath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Vector3 attitude = pilotView.transform.rotation.eulerAngles;
            WriteData(
                videoPlayer.frame.ToString()
                + "," + attitude.x.ToString()
                + "," + attitude.y.ToString()
                + "," + attitude.z.ToString(),
                filepath
            );
        }
    }

    static void WriteData(string data, string path)
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(data);
        writer.Close();
    }
}
