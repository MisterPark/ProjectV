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

    public UnityEvent OnDead;
    public UnityEvent<float> OnTakeDamage;
    public UnityEvent<int> OnLevelUp;
    public UnityEvent OnAddOrIncreaseSkill;

    [HideInInspector] public Stat stat;

    protected Animator animator;
    public CapsuleCollider capsuleCollider;
    
    Vector3 oldPosition;
    public Vector3 skillOffsetPosition;
    public Team team;
    List<Skill> skillList = new List<Skill>();

    float freezeTime;
    float freezeTick = 0;
    bool freezeFlag = false;

    float knockbackTick = 0;
    float knockbackDelay = 0.5f;
    bool knockbackFlag = false;

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
        stat.OnLevelUp.AddListener(OnStatLevelUp);
        stat.OnTakeDamage.AddListener(OnStatTakeDamage);
    }

    protected virtual void FixedUpdate()
    {
        ProcessFreeze();
        ProcessKnockback();
        Animation();
    }
    
    protected virtual void Update()
    {
        
    }

    /// <summary>
    /// target 위치로 이동, 회전 합니다.
    /// </summary>
    public void MoveTo(Vector3 target)
    {
        if (freezeFlag) return;
        Vector3 to = target - transform.position;
        Vector3 direction = to.normalized;
        
        transform.position += stat.Get_FinalStat(StatType.MoveSpeed) * direction * Time.fixedDeltaTime;
        transform.LookAt(target);
    }

    private void AddSkill(SkillKind kind)
    {
        Skill skill = null;
        
        switch (kind)
        {
            case SkillKind.IceBolt: skill = gameObject.AddComponent<Skill_IceBolt>(); break;
            case SkillKind.FireBolt: skill = gameObject.AddComponent<Skill_FireBolt>(); break;
            case SkillKind.ForceFieldBarrier: skill = gameObject.AddComponent<Skill_ForceFieldBarrier>(); break;
            case SkillKind.BlackHole: skill = gameObject.AddComponent<Skill_BlackHole>(); break;
            case SkillKind.Laser: skill = gameObject.AddComponent<Skill_Laser>(); break;
            case SkillKind.FireTornado: skill = gameObject.AddComponent<Skill_FireTornado>(); break;
            case SkillKind.ToxicTotem: skill = gameObject.AddComponent<Skill_ToxicTotem>(); break;
            case SkillKind.ShurikenAttack: skill = gameObject.AddComponent<Skill_ShurikenAttack>(); break;
            case SkillKind.Lightning: skill = gameObject.AddComponent<Skill_Lightning>(); break;
            case SkillKind.Rain: skill = gameObject.AddComponent<Skill_Rain>(); break;
            case SkillKind.Strength: skill = gameObject.AddComponent<Skill_Strength>(); break;
            case SkillKind.Range: skill = gameObject.AddComponent<Skill_Range>(); break;
            case SkillKind.MoveSpeed: skill = gameObject.AddComponent<Skill_MoveSpeed>(); break;
            case SkillKind.Speed: skill = gameObject.AddComponent<Skill_Speed>(); break;
            case SkillKind.MaxHp: skill = gameObject.AddComponent<Skill_MaxHp>(); break;
            case SkillKind.Armor: skill = gameObject.AddComponent<Skill_Armor>(); break;
            case SkillKind.Growth: skill = gameObject.AddComponent<Skill_Growth>(); break;
            case SkillKind.Magnet: skill = gameObject.AddComponent<Skill_Magnet>(); break;
            case SkillKind.BlizzardOrb: skill = gameObject.AddComponent<Skill_BlizzardOrb>(); break;
            case SkillKind.UnstableMagicMissile: skill = gameObject.AddComponent<Skill_UnstableMagicMissile>(); break;
            case SkillKind.HeavyFireBall: skill = gameObject.AddComponent<Skill_HeavyFireBall>(); break;
            case SkillKind.Meteor: skill = gameObject.AddComponent<Skill_Meteor>(); break;
            case SkillKind.IceTornado: skill = gameObject.AddComponent<Skill_IceTornado>(); break;
            case SkillKind.ToxicTornado: skill = gameObject.AddComponent<Skill_ToxicTornado>(); break;
            case SkillKind.WindTornado: skill = gameObject.AddComponent<Skill_WindTornado>(); break;
            case SkillKind.LavaOrb: skill = gameObject.AddComponent<Skill_LavaOrb>(); break;
            default:
                break;
        }

        if(skill != null)
        {
            skillList.Add(skill);
        }
    }
    /// <summary>
    /// 스킬이 없으면 추가, 있으면 레벨업
    /// </summary>
    public void AddOrIncreaseSkill(SkillKind kind)
    {
        Skill skill = FindSkill(kind);
        if (skill != null)
        {
            skill.LevelUp();
        }
        else
        {
            AddSkill(kind);
        }
        OnAddOrIncreaseSkill.Invoke();
    }
    /// <summary>
    /// 스킬을 삭제합니다.
    /// </summary>
    public void RemoveSkill(SkillKind kind)
    {
        Skill skill = null;

        switch (kind)
        {
            case SkillKind.IceBolt: skill = gameObject.GetComponent<Skill_IceBolt>(); break;
            case SkillKind.FireBolt: skill = gameObject.GetComponent<Skill_FireBolt>(); break;
            case SkillKind.ForceFieldBarrier: skill = gameObject.GetComponent<Skill_ForceFieldBarrier>(); break;
            case SkillKind.BlackHole: skill = gameObject.GetComponent<Skill_BlackHole>(); break;
            case SkillKind.Laser: skill = gameObject.GetComponent<Skill_Laser>(); break;
            case SkillKind.FireTornado: skill = gameObject.GetComponent<Skill_FireTornado>(); break;
            case SkillKind.ToxicTotem: skill = gameObject.GetComponent<Skill_ToxicTotem>(); break;
            case SkillKind.ShurikenAttack: skill = gameObject.GetComponent<Skill_ShurikenAttack>(); break;
            case SkillKind.Lightning: skill = gameObject.GetComponent<Skill_Lightning>(); break;
            case SkillKind.Rain: skill = gameObject.GetComponent<Skill_Rain>(); break;
            case SkillKind.Strength: skill = gameObject.GetComponent<Skill_Strength>(); break;
            case SkillKind.Range: skill = gameObject.GetComponent<Skill_Range>(); break;
            case SkillKind.MoveSpeed: skill = gameObject.GetComponent<Skill_MoveSpeed>(); break;
            case SkillKind.Speed: skill = gameObject.GetComponent<Skill_Speed>(); break;
            case SkillKind.MaxHp: skill = gameObject.GetComponent<Skill_MaxHp>(); break;
            case SkillKind.Armor: skill = gameObject.GetComponent<Skill_Armor>(); break;
            case SkillKind.Growth: skill = gameObject.GetComponent<Skill_Growth>(); break;
            case SkillKind.Magnet: skill = gameObject.GetComponent<Skill_Magnet>(); break;
            case SkillKind.BlizzardOrb: skill = gameObject.GetComponent<Skill_BlizzardOrb>(); break;
            case SkillKind.UnstableMagicMissile: skill = gameObject.GetComponent<Skill_UnstableMagicMissile>(); break;
            case SkillKind.Meteor: skill = gameObject.GetComponent<Skill_Meteor>(); break;
            default:
                break;
        }

        if(skill != null)
        {
            skillList.Remove(skill);
            Destroy(skill);
        }
    }
    /// <summary>
    /// 해당 종류의 스킬을 찾습니다. 없으면 NULL 반환
    /// </summary>
    public Skill FindSkill(SkillKind kind)
    {
        int count = Skills.Count;
        for (int i = 0; i < count; i++)
        {
            Skill skill = Skills[i];
            if (skill.Kind == kind)
            {
                return skill;
            }
        }

        return null;
    }
    /// <summary>
    /// time만큼 멈춥니다. (초단위)
    /// </summary>
    /// <param name="time"></param>
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

    void ProcessKnockback()
    {
        if (knockbackFlag == false) return;

        knockbackTick += Time.fixedDeltaTime;
        if(knockbackTick > knockbackDelay)
        {
            knockbackTick = 0f;
            knockbackFlag = false;
        }
    }

    void Animation()
    {
        if (animator == null) return;

        animator.speed = (freezeFlag ? 0 : 1);
        animator.SetInteger("UnitType", (int)type);
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
        Color fontColor = Color.white;
        Color outlineColor = Color.black;

        if(gameObject.IsPlayer())
        {
            fontColor = Color.red;
            outlineColor = Color.yellow;
        }
        else
        {
            float clamp = Mathf.Clamp(damage, 0f, 255f);
            fontColor.r = 1f;
            fontColor.g = 1f - (clamp / 255f);
            fontColor.b = 0f;
        }

        font.Init((int)damage, fontColor, outlineColor, transform.position + (Vector3.up * 2f));

        float hp = stat.Get_FinalStat(StatType.Health);
        if (hp <= 0f)
        {
            Death();
            OnDead?.Invoke();
        }
    }

    void Death()
    {
        if(type == UnitType.Monster)
        {
            GameObject deathObject = ObjectPool.Instance.Allocate($"{gameObject.name}_Death");
            deathObject.transform.position = transform.position;
            deathObject.transform.rotation = transform.rotation;
        }
        
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
    /// <summary>
    /// 스킬 데이터를 업데이트 합니다.
    /// </summary>
    public void UpdateSkillData()
    {
        foreach (var skill in skillList)
        {
            if (skill.Type == SkillType.Passive) continue;
            skill.UpdateSkillData();
        }
    }

    /// <summary>
    /// 유닛을 밀어냅니다.
    /// </summary>
    /// <param name="sourcePosition">밀어내는 위치</param>
    /// <param name="power">힘</param>
    public void Knockback(Vector3 sourcePosition, float power)
    {
        if (knockbackFlag) return;
        knockbackFlag = true;
        Vector3 direction = transform.position - sourcePosition;
        transform.position += direction.normalized * power;
        transform.LookAt(sourcePosition);
    }

    void OnStatTakeDamage(float damage)
    {
        OnTakeDamage?.Invoke(damage);
    }

    void OnStatLevelUp(int level)
    {
        OnLevelUp?.Invoke(level);
    }
}
