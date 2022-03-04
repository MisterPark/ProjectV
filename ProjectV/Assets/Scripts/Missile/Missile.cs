using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MissileType // ¿òÁ÷ÀÓ
{
    Directional,
    Guided,
}

public class Missile : MonoBehaviour
{
    public Team team;
    public MissileType type;
    public float duration;
    public float speed;
    public float damage;

    public Unit owner;
    GameObject target;
    Vector3 targetDirection;
    float tick = 0f;

    public UnityEvent<Vector3> OnCollision;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if(tick >= duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
            return;
        }

        ProcessMove();
        ProcessRotate();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.IsPlayer()) return;
        Unit unit = other.gameObject.GetComponent<Unit>();
        if(unit != null)
        {
            if( team != unit.team)
            {
                unit.stat.TakeDamage(damage);
                OnCollision?.Invoke(transform.position);
                ObjectPool.Instance.Free(gameObject);
            }
        }
    }

    public void Initialize()
    {
        tick = 0f;
    }

    void ProcessMove()
    {
        if(type == MissileType.Guided)
        {
            if(target != null)
            {
                Vector3 to = target.transform.position - transform.position;
                targetDirection = to.normalized;
            }
        }

        transform.position += targetDirection * speed * Time.fixedDeltaTime;
    }

    void ProcessRotate()
    {
        Vector3 targetPos = Vector3.zero;
        switch (type)
        {
            case MissileType.Directional:
                targetPos = transform.position + targetDirection;
                break;
            case MissileType.Guided:
                {
                    if(target != null)
                    {
                        targetPos = target.transform.position;
                    }
                }
                break;
            default:
                targetPos = transform.position + targetDirection;
                break;
        }

        transform.LookAt(targetPos);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetTarget(Vector3 target)
    {
        Vector3 to = target - transform.position;
        targetDirection = to.normalized;
    }
}
