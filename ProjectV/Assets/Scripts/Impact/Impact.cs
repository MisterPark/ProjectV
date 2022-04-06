using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviourEx
{
    ParticleSystem particle;

    protected override void Awake()
    {
        base.Awake();
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
    }

    public override void FixedUpdateEx()
    {
        if(particle == null)
        {
            particle = GetComponent<ParticleSystem>();
        }
        if (particle == null)
            return;

        if(particle.isStopped)
        {
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
