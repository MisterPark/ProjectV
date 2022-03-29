using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDelete : MonoBehaviour
{
    float lifeTime=0.1f;
    float tick=0f;
    
    void Start()
    {
        
    }

    private void OnEnable()
    {
        tick = 0;
    }
    
    void FixedUpdate()
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
