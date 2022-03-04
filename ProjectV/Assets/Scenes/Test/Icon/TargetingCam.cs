using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCam : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float distance = 2;
    [SerializeField] Vector3 direction;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 dir = direction.normalized;

        transform.position = target.transform.position + dir * distance;
        transform.LookAt(target.transform);
    }
}
