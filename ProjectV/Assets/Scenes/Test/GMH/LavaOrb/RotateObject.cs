using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    
    [SerializeField] float rotationSpeed=5f;
    [SerializeField] bool RotateX = false;
    [SerializeField] bool RotateY = false;
    [SerializeField] bool RotateZ = false;
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        if (RotateX)
        { transform.Rotate(Vector3.right, rotationSpeed*Time.fixedDeltaTime, Space.World); }
        if (RotateY)
        { transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime, Space.World); }
        if (RotateZ)
        { transform.Rotate(Vector3.forward, rotationSpeed * Time.fixedDeltaTime, Space.World); }

    }
}
