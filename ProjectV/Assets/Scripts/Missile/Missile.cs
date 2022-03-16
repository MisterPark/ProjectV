using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
public enum MissileType // ¿òÁ÷ÀÓ
{
    Directional,
    Guided,
    Other,
}

public class Missile : MonoBehaviour
{
    public Team team;
    public MissileType type;

    public float duration;
    public float speed;
    public float damage;
    public float delay;
    public float range;
    public bool isPull = false;
    public bool isPenetrate = false;
    public bool isRotate=false;
    public float knockbackPower = 1f;
    public bool KnockbackFlag { get; set; } = false;
    public bool AttackFlag { get; set; } = false;
    public Vector3 targetDirection { get; set; }
    public Unit owner;
    public GameObject target;
    protected float tick = 0f;
    protected float cooltimeTick;
    protected VisualEffect visualEffect;
    
    
    bool isWaitForFrame = false;

    public UnityEvent<Vector3, Unit> OnCollision;

    protected virtual void Start()
    {
        transform.localScale = Vector3.one * range;
    }
    protected virtual void OnEnable()
    {
        visualEffect = GetComponentInChildren<VisualEffect>();
        transform.localScale = Vector3.one * range;

        if (visualEffect != null)
        {
            visualEffect.SetFloat("Duration", duration);
        }

        tick = 0;
        cooltimeTick = 0;
        isWaitForFrame = false;
        AttackFlag = false;
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
        if (delay != 0)
        {
            cooltimeTick += Time.fixedDeltaTime;
            if (cooltimeTick >= delay)
            {
                cooltimeTick = 0f;
                AttackFlag = true;
            }
        }
        ProcessMove();
        ProcessRotate();
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.IsPlayer()) return;
        Unit unit = other.gameObject.GetComponent<Unit>();
        if(unit != null)
        {
            if( team != unit.team)
            {
                OnCollision?.Invoke(transform.position, unit);
                if (!isPenetrate)
                {
                    unit.stat.TakeDamage(damage);
                    if (KnockbackFlag)
                    {
                        unit.Knockback(transform.position, 1);
                    }
                    ObjectPool.Instance.Free(gameObject); 
                }
                else if (isPenetrate)
                {
                    if (AttackFlag)
                    {
                        unit.stat.TakeDamage(damage);
                        if (KnockbackFlag)
                        {
                            unit.Knockback(transform.position, 1);
                        }
                    }
                }
                if (isPull)
                {
                    
                    
                    Vector3 direction = transform.position - unit.transform.position;
                    float pullPower = 2f;
                    unit.transform.position += direction * pullPower * Time.fixedDeltaTime;
                }
                
            }
        }
    }
    protected virtual void LateUpdate()
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
    public void Initialize()
    {
        tick = 0f;
    }

    protected virtual void ProcessMove()
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
        if(!isRotate)
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
