using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public bool enabled = true;
    public GameObject cameraRig;
    public bool debugMode = false;
    public Text logOut;
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
            transform.localRotation = gyro.attitude * rot;
        }
        
        if (debugMode)
        {
            logOut.text = (transform.rotation.eulerAngles.ToString());
        }
    }
}
