using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviourEx
{
    ParticleSystem particle;
    float tick = 0;

    protected override void Start()
    {
        base.Start();
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        tick = 0;
    }

    public override void FixedUpdateEx()
    {
        tick += Time.fixedDeltaTime;
        if(particle.isStopped)
        {
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
