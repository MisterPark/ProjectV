using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ImpactV2 : MonoBehaviourEx
{
    public float Duration { get; set; } = 0.2f;
    float tick = 0f;
    VisualEffect effect;
    protected override void Start()
    {
        base.Start();
        effect = GetComponentInChildren<VisualEffect>();
        effect.SetFloat("Duration", Duration);
    }

    private void OnEnable()
    {
        tick = 0f;
        
    }

    public override void FixedUpdateEx()
    {
        tick += Time.fixedDeltaTime;
        if(tick > Duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
