using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public bool enabled = true;
    public bool debugMode = false;
    public Text logOut;
    public Quaternion gyroValue = new Quaternion(0, 0, 0, 1);
    public Quaternion airplaneAttitude = new Quaternion(0, 0, 0, 1);
    private bool gyroEnabled;
    private Gyroscope gyro;

    private Quaternion rot;
 
    // Start is called before the first frame update
    void Start()
    {
        if (enabled)
        {
            gyroEnabled = EnableGyro();
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled && gyroEnabled)
        {
            gyroValue = gyro.attitude;

            // Aircraft Principal Axes
            // https://en.wikipedia.org/wiki/Aircraft_principal_axes
            // Typical Roll, Pitch, Yaw system is based on right-handed coordinate system, and -Z up, X forward.
            // Here we convert gyroscope value to fit this coordinate.
            airplaneAttitude = new Quaternion(gyroValue.y, gyroValue.x, -gyroValue.z, gyroValue.w);
            airplaneAttitude = airplaneAttitude * new Quaternion(0, 0.7f, 0, -0.7f);

            // In order to control camera by gyroscope,
            // 1. convert right-handed coordinate system to left-handed
            // 2. rotate value 90 degree along X axis. (because the default gyroscope attitude is facing down,
            //    while that of unity camera is facing horizontal)
            Quaternion gyroValueInLeftHanded = new Quaternion(-gyroValue.x, -gyroValue.z, -gyroValue.y, gyroValue.w);
            transform.localRotation = gyroValueInLeftHanded * new Quaternion(0.7f, 0, 0, 0.7f);
        }
        
        if (debugMode)
        {
            logOut.text = airplaneAttitude.ToString();
        }
    }
}
