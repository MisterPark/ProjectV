using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnitType
{
    None,
    Player,
    Monster,
    Prop,
}


public class Unit : MonoBehaviour
{
    public UnitType type;
    // �̺�Ʈ
    public UnityEvent OnDead;
    public UnityEvent<float> OnTakeDamage;
    public UnityEvent<int> OnLevelUp;
    // ����
    [HideInInspector] public Stat stat;

    // ������Ʈ
    protected Animator animator;
    public CapsuleCollider capsuleCollider;
    
    // ����
    Vector3 oldPosition;
    public Vector3 skillOffsetPosition;
    public Team team;
    List<Skill> skillList = new List<Skill>();

    float freezeTime;
    float freezeTick = 0;
    bool freezeFlag = false;

    public List<Skill> Skills { get { return skillList; } }

    protected virtual void Start()
    {
        stat = GetComponent<Stat>();
        animator = GetComponentInChildren<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator Not Found");
        }
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        if(capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider Not Found");
        }
        oldPosition = transform.position;
        OnTakeDamage.AddListener(OnTakeDamageCallback);
        OnLevelUp.AddListener(OnLevelUpCallback);
    }

    protected virtual void FixedUpdate()
    {
        ProcessFreeze();
        Animation();
    }
    
    protected virtual void Update()
    {
        
    }


    public void MoveTo(Vector3 target)
    {
        if (freezeFlag) return;
        Vector3 to = target - transform.position;
        Vector3 direction = to.normalized;
        
        transform.position += stat.Get_FinalStat(StatType.MoveSpeed) * direction * Time.fixedDeltaTime;
        transform.LookAt(target);
    }

    public void AddSkill(SkillKind type)
    {
        Skill skill = null;
        
        switch (type)
        {
            case SkillKind.IceBolt: skill = gameObject.AddComponent<Skill_IceBolt>(); break;
            case SkillKind.FireBolt: skill = gameObject.AddComponent<Skill_FireBolt>(); break;
            case SkillKind.ForceFieldBarrier: skill = gameObject.AddComponent<Skill_ForceFieldBarrier>(); break;
            case SkillKind.BlackHole: skill = gameObject.AddComponent<Skill_BlackHole>(); break;
            case SkillKind.Laser: skill = gameObject.AddComponent<Skill_Laser>(); break;
            case SkillKind.FireTornado: skill = gameObject.AddComponent<Skill_FireTornado>(); break;
            case SkillKind.RockTotem: skill = gameObject.AddComponent<Skill_RockTotem>(); break;
            case SkillKind.ShurikenAttack: skill = gameObject.AddComponent<Skill_ShurikenAttack>(); break;
            case SkillKind.Lightning: skill = gameObject.AddComponent<Skill_Lightning>(); break;
            case SkillKind.Rain: skill = gameObject.AddComponent<Skill_Rain>(); break;
            default:
                break;
        }

        if(skill != null)
        {
            skillList.Add(skill);
        }
    }

    public void Freeze(float time)
    {
        freezeTime = time;
        freezeTick = 0;
        freezeFlag = true;
    }


    void ProcessFreeze()
    {
        if (freezeFlag == false) return;
        
        freezeTick += Time.fixedDeltaTime;
        if(freezeTick > freezeTime)
        {
            freezeTick = 0f;
            freezeFlag = false;
        }
        
    }

    void Animation()
    {
        if (animator == null) return;

        animator.speed = (freezeFlag ? 0 : 1);
        // ���� Ÿ�� ����
        animator.SetInteger("UnitType", (int)type);
        // �޸���
        bool isRun = oldPosition != transform.position;
        if (isRun)
        {
            oldPosition = transform.position;
        }
        animator.SetBool("IsRun", isRun);


    }

    void OnTakeDamageCallback(float damage)
    {

        GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
        UI_DamageFont font = temp.transform.GetChild(0).GetComponent<UI_DamageFont>();
        font.Init((int)damage, UI_DamageFont.FontColor.WHITE, transform.position + (Vector3.up * 2f));

        // ���ó��
        float hp = stat.Get_FinalStat(StatType.Health);
        if (hp <= 0f)
        {
            Death();
            OnDead?.Invoke();
        }
    }

    void Death()
    {
        GameObject deathObject = ObjectPool.Instance.Allocate($"{gameObject.name}_Death");
        deathObject.transform.position = transform.position;
        deathObject.transform.rotation = transform.rotation;
    }

    void OnLevelUpCallback(int level)
    {

    }

    private void OnEnable()
    {
        if(stat != null)
        {
            stat.RecoverToFull();
        }
    }
}
