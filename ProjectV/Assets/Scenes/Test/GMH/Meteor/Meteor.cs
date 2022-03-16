using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Board")
        {
            GameObject obj = ObjectPool.Instance.Allocate("MeteorHit");
            obj.transform.position=new Vector3(gameObject.transform.position.x,0f, gameObject.transform.position.z);
        }
    }
}