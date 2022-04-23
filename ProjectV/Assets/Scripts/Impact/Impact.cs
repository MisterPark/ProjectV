using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviourEx, IFixedUpdater
{
    ParticleSystem particle;

    protected override void Awake()
    {
        base.Awake();
        particle = GetComponent<ParticleSystem>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public void FixedUpdateEx()
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
