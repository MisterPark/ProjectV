using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
public enum MissileType // ������
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
    public float delay;
    public float range;
    public bool isPull = false;
    public bool isPenetrate = false;
    public bool isRotate=false;
    public bool AttackFlag { get; set; } = false;
    public Vector3 colliderSize;
    public Vector3 colliderCenter;
    public Unit owner;
    GameObject target;
    Vector3 targetDirection;
    float tick = 0f;
    float cooltimeTick;
    VisualEffect visualEffect;
    
    
    bool isWaitForFrame = false;

    public UnityEvent<Vector3, Unit> OnCollision;

    void Start()
    {
        transform.localScale = Vector3.one * range;
    }
    private void OnEnable()
    {
        visualEffect = GetComponentInChildren<VisualEffect>();
        transform.localScale = Vector3.one * range;

        if (visualEffect != null)
        {
            visualEffect.SetFloat("Duration", duration);
        }
        BoxCollider boxCollider = GetComponentInParent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        if (boxCollider != null)
        {
            boxCollider.size = colliderSize;
            boxCollider.center = colliderCenter;
        }


       
        tick = 0;
        cooltimeTick = 0;
        isWaitForFrame = false;
        AttackFlag = false;
    }

    void FixedUpdate()
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

    private void OnTriggerStay(Collider other)
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
                    ObjectPool.Instance.Free(gameObject); 
                }
                else if (isPenetrate)
                {
                    if (AttackFlag)
                    {
                        unit.stat.TakeDamage(damage);
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
