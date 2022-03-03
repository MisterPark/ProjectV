using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        Vector3 camForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        transform.forward = camForward.normalized;

    }
}
