using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissileType // 움직임
{
    Directional,
    Guided,
}

public class Missile : MonoBehaviour
{
    public Team team;
    [SerializeField]public MissileType type;
    [SerializeField]public float duration;
    [SerializeField]public float speed;
    [SerializeField]public float damage;

    public Unit owner;
    GameObject target;
    Vector3 targetDirection;
    float tick = 0f;
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
                // 임시
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
