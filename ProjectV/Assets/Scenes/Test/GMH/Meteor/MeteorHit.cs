using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHit : MonoBehaviour
{

    public float lifeTime;
    public float downSpeed;
    private float tick;
    
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        transform.position += new Vector3(0, downSpeed*Time.fixedDeltaTime, 0);
        if (tick > lifeTime/4)
        {
            transform.position += new Vector3(0, downSpeed * Time.fixedDeltaTime*5, 0);
        }

            if (tick > lifeTime)
        {
            tick = 0;
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
