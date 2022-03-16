using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class SkillObject : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * range;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnEnable()
    {
        tick = 0;
        cooltimeTick = 0;
    }

    public void Initialize()
    {
        tick = 0f;
    }

    protected virtual void FixedUpdate()
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
