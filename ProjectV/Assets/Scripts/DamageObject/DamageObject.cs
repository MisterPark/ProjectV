using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageObject : MonoBehaviour
{
    public bool isPull = false;
    
    public float damage;
    public bool AttackFlag { get; set; } = false;
    public float duration;
    public float delay;
    public float speed;
    public bool isGuided = false;
    bool isWaitForFrame = false;
    float tick;
    float cooltimeTick;

    GameObject target;
    Vector3 targetDirection;
    public UnityEvent<Vector3> OnCollision;
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

        ProcessMove();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.IsPlayer()) return;
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null)
        {
            if (AttackFlag)
            {
                OnCollision?.Invoke(other.transform.position);
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
    void ProcessMove()
    {
        if (isGuided)
        {
            if (target != null)
            {
                Vector3 to = target.transform.position - transform.position;
                targetDirection = to.normalized;
            }
        }

        transform.position += targetDirection * speed * Time.fixedDeltaTime;
    }

    public void SetTarget(Vector3 target)
    {
        Vector3 to = target - transform.position;
        targetDirection = to.normalized;
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
