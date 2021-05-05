using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCameraRotation : MonoBehaviour
{
    public GameObject mainCamera;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
