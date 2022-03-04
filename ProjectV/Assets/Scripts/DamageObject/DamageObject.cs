using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public bool isPull = false;
    
    public float damage;
    public bool AttackFlag { get; set; } = false;
    public float duration;
    public float delay;
    bool isWaitForFrame = false;    
    float tick;
    float cooltimeTick;
    void Start()
    {

    }

    private void OnEnable()
    {
        tick = 0;
        cooltimeTick = 0;
        isWaitForFrame= false;
        AttackFlag = false;
    }
    private void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        cooltimeTick += Time.fixedDeltaTime;
        if (tick >= duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
            return;
        }

        if(cooltimeTick >= delay)
        {
            cooltimeTick = 0f;
            AttackFlag = true;
            return;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.IsPlayer()) return;
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null)
        {
            if (AttackFlag)
            {
                unit.stat.TakeDamage(damage);
                
            }
            if (isPull)
            {
                Vector3 direction = transform.position - unit.transform.position;
                float pullPower = 2f;
                unit.transform.position += direction * pullPower * Time.fixedDeltaTime;
            }
        }

    }

    private void LateUpdate()
    {
        if (isWaitForFrame)
        {
            isWaitForFrame = false;
            this.AttackFlag = false;
        }

        if (this.AttackFlag)
        {
            isWaitForFrame = true;
        }
    }
}
