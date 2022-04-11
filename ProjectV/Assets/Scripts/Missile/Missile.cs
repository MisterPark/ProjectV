using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
public enum MissileType // ¿òÁ÷ÀÓ
{
    Directional,
    Guided,
    Attached,
    Other,
}

public class Missile : MonoBehaviourEx
{
    private float duration;
    private float speed;
    private float damage;
    private float delay;
    private float range;
    private bool isPull = false;
    private bool isPenetrate = false;
    private bool isRotate = false;
    private float knockbackPower = 1f;

    public Team Team { get; set; }
    public MissileType Type { get; set; }
    public float Duration 
    {
        get { return duration; } 
        set 
        { 
            duration = value;
            visualEffect = GetComponentInChildren<VisualEffect>();
            if (visualEffect != null)
            {
                visualEffect.SetFloat("Duration", value);
            }
        } 
    }
    public float Speed { get { return speed; } set { speed = value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public float Delay { get { return delay; } set { delay = value; } }
    public float Range { get { return range; } set { range = value; } }
    public bool IsPull { get { return isPull; } set { isPull = value; } }
    public bool IsPenetrate { get { return isPenetrate; } set { isPenetrate = value; } }
    public bool IsRotate { get { return isRotate; } set { isRotate = value; } }
    public float KnockbackPower { get { return knockbackPower; } set { knockbackPower = value; } }
    public bool KnockbackFlag { get; set; } = false;
    public bool AttackFlag { get; set; } = false;
    public Vector3 TargetDirection { get; set; }
    public Unit Owner { get; set; }
    public GameObject Target { get; set; }

    protected float tick = 0f;
    protected float cooltimeTick;
    protected VisualEffect visualEffect;


    bool isWaitForFrame = false;

    public UnityEvent<Vector3, Unit> OnCollision;

    public static Dictionary<GameObject, Missile> Missiles = new Dictionary<GameObject, Missile>();

    protected override void Awake()
    {
        base.Awake();
        Missile.Missiles.Add(gameObject, this);
    }

    protected override void Start()
    {
        base.Start();
        transform.localScale = Vector3.one * Range;
    }
    protected virtual void OnEnable()
    {
        visualEffect = GetComponentInChildren<VisualEffect>();
        transform.localScale = Vector3.one * Range;

        if (visualEffect != null)
        {
            visualEffect.SetFloat("Duration", Duration);
        }

        tick = 0;
        cooltimeTick = 0;
        isWaitForFrame = false;
        AttackFlag = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Missiles.Remove(gameObject);
    }

    public static Missile Find(GameObject obj)
    {
        Missile missile;
        if(Missiles.TryGetValue(obj, out missile) == false)
        {
            Debug.LogError("Unregistered Missile");
        }
        return missile;
    }

    public override void FixedUpdateEx()
    {
        tick += Time.fixedDeltaTime;
        if (tick >= Duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
            return;
        }
        if (Delay != 0)
        {
            cooltimeTick += Time.fixedDeltaTime;
            if (cooltimeTick >= Delay)
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
        Unit unit = Unit.Find(other.gameObject);
        if (unit != null)
        {
            if (Team != unit.team)
            {
                if (!IsPenetrate)
                {
                    unit.stat.TakeDamage(Damage);
                    OnCollision?.Invoke(transform.position, unit);
                    if (KnockbackFlag)
                    {
                        unit.Knockback(transform.position, 0.2f);
                    }
                    ObjectPool.Instance.Free(gameObject);
                }
                else if (IsPenetrate)
                {
                    if (AttackFlag)
                    {
                        unit.stat.TakeDamage(Damage);
                        OnCollision?.Invoke(transform.position, unit);
                        if (KnockbackFlag)
                        {
                            unit.Knockback(transform.position, 0.2f);
                        }
                    }
                }
                if (IsPull)
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

    public void Destroy()
    {
        ObjectPool.Instance.Free(this.gameObject);
    }

    protected virtual void ProcessMove()
    {
        if (Type == MissileType.Guided)
        {
            if (Target != null)
            {
                Vector3 to = Target.transform.position - transform.position;
                TargetDirection = to.normalized;
            }
        }
        else if (Type == MissileType.Attached)
        {
            TargetDirection = Vector3.zero;
            if (Target != null)
            {
                transform.position = Target.transform.position;
            }
        }

        transform.position += TargetDirection * Speed * Time.fixedDeltaTime;
    }

    void ProcessRotate()
    {
        Vector3 targetPos = Vector3.zero;
        switch (Type)
        {
            case MissileType.Directional:
                targetPos = transform.position + TargetDirection;
                break;
            case MissileType.Guided:
                {
                    if (Target != null)
                    {
                        targetPos = Target.transform.position;
                    }
                }
                break;
            case MissileType.Attached:
                {
                    if (Target != null)
                    {
                        targetPos = Target.transform.forward + Target.transform.position;
                    }
                    break;
                }
            default:
                targetPos = transform.position + TargetDirection;
                break;
        }
        if (!IsRotate)
            transform.LookAt(targetPos);
    }

    public void SetTarget(GameObject target)
    {
        this.Target = target;
    }

    public void SetTarget(Vector3 target)
    {
        Vector3 to = target - transform.position;
        TargetDirection = to.normalized;
    }

}
