using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ImpactV2 : MonoBehaviour
{
    public float Duration { get; set; } = 0.2f;
    float tick = 0f;
    VisualEffect effect;
    void Start()
    {
        effect = GetComponentInChildren<VisualEffect>();
        effect.SetFloat("Duration", Duration);
    }

    private void OnEnable()
    {
        tick = 0f;
        
    }

    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if(tick > Duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
