using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactV2 : MonoBehaviour
{
    float duration = 0.2f;
    float tick = 0f;
    void Start()
    {
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
