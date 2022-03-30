using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDelete : MonoBehaviourEx
{
    float lifeTime=0.1f;
    float tick=0f;
    
    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        tick = 0;
    }
    
    public override void FixedUpdateEx()
    {
        tick += Time.fixedDeltaTime;
        if (lifeTime < tick)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit == null)
        { return; }

        if (unit.type == UnitType.Monster)
        {
            unit.stat.TakeDamage(9999); 
        }
    }
}
