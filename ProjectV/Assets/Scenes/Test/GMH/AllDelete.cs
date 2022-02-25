using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDelete : MonoBehaviour
{
    float lifeTime=0.1f;
    float tick=0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        tick = 0;
    }
    // Update is called once per frame
    void Update()
    {
        tick += Time.deltaTime;
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
