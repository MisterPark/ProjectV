using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMove : MonoBehaviour
{
    [SerializeField] float speed = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
