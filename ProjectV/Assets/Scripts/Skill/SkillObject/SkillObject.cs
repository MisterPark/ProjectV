using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class SkillObject : MonoBehaviourEx, IFixedUpdater
{
    public Team team;
    public float duration;
    public float speed;
    public float damage;
    public float delay;
    public float range;

    public Unit owner;
    public float tick = 0f;
    public float cooltimeTick;
    
    protected override void Start()
    {
        base.Start();
        transform.localScale = Vector3.one * range;
    }

    

    protected override void OnEnable()
    {
        base.OnEnable();
        tick = 0;
        cooltimeTick = 0;
    }

    public void Initialize()
    {
        tick = 0f;
    }

    public virtual void FixedUpdateEx()
    {
        tick += Time.fixedDeltaTime;
        if (tick >= duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
            return;
        }
    }
}
