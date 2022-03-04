using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFire
    : MonoBehaviour
{
    public float damage;
    public float duration;
    public float delay;
    float tick = 0;
    float attackTick = 0;
    bool isAttack = false;


    void Start()
    {
        
    }

    private void OnEnable()
    {
        isAttack = false;
        attackTick = 0;
        tick = 0;
    }


    void FixedUpdate()
    {
        attackTick += Time.fixedDeltaTime;
        if(attackTick >= delay)
        {
            attackTick = 0;
            isAttack = true;
        }
        else
        {
            //isAttack = false;
        }

        tick += Time.fixedDeltaTime;
        if(tick >= duration)
        {
            isAttack = false;
            attackTick = 0;
            tick = 0;
            ObjectPool.Instance.Free(gameObject);
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.IsPlayer()) return;
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null)
        {
            if(isAttack)
            {
                unit.stat.TakeDamage(damage);
            }
        }

    }
}
