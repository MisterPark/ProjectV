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

    // Update is called once per frame
    void Update()
    {
        if (RotateX)
        { transform.Rotate(Vector3.right, rotationSpeed*Time.deltaTime, Space.World); }
        if (RotateY)
        { transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World); }
        if (RotateZ)
        { transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.World); }

    }
}
