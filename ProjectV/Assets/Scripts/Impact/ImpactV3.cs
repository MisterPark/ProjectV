using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactV3 : MonoBehaviourEx
{
    float tick = 0;
    float duration = 1f;
    Unit target;
    public float Duration { get { return duration; } set { duration = value; } }
    public Unit Target
    {
        get { return target; }
        set
        {
            target = value;
            if (target != null)
            {
                target.OnDead.RemoveListener(Remove);
                target.OnDead.AddListener(Remove);
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        tick = 0;

    }

    private void OnEnable()
    {
        tick = 0f;
    }

    public override void FixedUpdateEx()
    {
        tick += Time.fixedDeltaTime;
        if(tick > duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
        }
    }

    public void Remove()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
