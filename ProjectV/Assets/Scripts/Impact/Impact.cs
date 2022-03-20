using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    ParticleSystem particle;
    float tick = 0;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        tick = 0;
    }

    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if(particle.isStopped)
        {
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
