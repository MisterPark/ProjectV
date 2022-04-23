using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDelete : MonoBehaviourEx, IFixedUpdater
{
    float lifeTime=0.1f;
    float tick=0f;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        tick = 0;
    }

    public void FixedUpdateEx()
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
        Unit unit;
        if (Unit.Units.TryGetValue(other.gameObject, out unit) == false) return;
        if (unit == null) return;

        if (unit.type == UnitType.Monster)
        {
            unit.stat.TakeDamage(9999); 
        }
    }

    
}
