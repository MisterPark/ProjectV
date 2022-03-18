using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactV3 : MonoBehaviour
{
    float tick = 0;
    float duration = 1f;
    public float Duration { get { return duration; }  set { duration = value; } }

    void Start()
    {
        tick = 0;

    }

    private void OnEnable()
    {
        tick = 0f;
    }

    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if(tick > duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
