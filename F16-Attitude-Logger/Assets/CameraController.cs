using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public bool enabled = true;
    public bool debugMode = false;
    public Text logOut;
    public Quaternion gyroValue = Quaternion.Euler(0, 0, 0);
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

            // iPhone's gyroscope is right-handed coordinate system, while unity is lefft-handed.
            // hence we need to convert it.
            Quaternion gyroValueInLeftHanded = new Quaternion(-gyroValue.x, -gyroValue.z, -gyroValue.y, gyroValue.w);

            // Since the attitude of no rotation (origin of rotation) in gyroscope is screen up (camer down)
            // while that of unity is +Z direction, we need to rotate gyroscope value 90 degree along X axis.
            transform.localRotation = gyroValueInLeftHanded * new Quaternion(0.7f, 0, 0, 0.7f);
        }
        
        if (debugMode)
        {
            //logOut.text = (transform.localRotation.ToString());
            logOut.text = transform.localRotation.eulerAngles.ToString();
        }
    }
}
