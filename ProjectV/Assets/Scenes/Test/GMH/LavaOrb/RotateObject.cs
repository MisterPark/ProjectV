using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
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
        { transform.Rotate(Vector3.right, rotationSpeed, Space.World); }
        if (RotateY)
        { transform.Rotate(Vector3.up, rotationSpeed, Space.World); }
        if (RotateZ)
        { transform.Rotate(Vector3.forward, rotationSpeed, Space.World); }

    }
}
