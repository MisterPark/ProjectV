using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed,Space.World);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
            return;
    }
}
