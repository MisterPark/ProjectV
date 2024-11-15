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
public class Unit : MonoBehaviourEx, IFixedUpdater
{
    public UnitType type;

    public UnityEvent OnDead;
    public UnityEvent<float> OnTakeDamage;
    public UnityEvent<int> OnLevelUp;
    public UnityEvent OnAddOrIncreaseSkill;

    [HideInInspector] public Stat stat;

    protected Animator animator;
    public CapsuleCollider capsuleCollider;
    public float Radius { get; set; }
    
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

    float damageFontTick = 0;
    float damageFontDelay = 0.3f;
    float damageSum = 0;
    bool damageFontFlag = false;

    public List<Skill> Skills { get { return skillList; } }

    public Sound[] Sounds { get; set; }

    public static Dictionary<GameObject, Unit> Units = new Dictionary<GameObject, Unit>();

    protected override void Awake()
    {
        base.Awake();
        Units.Add(gameObject, this);
        stat = GetComponent<Stat>();
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            //Debug.LogError("Animator Not Found");
        }
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider Not Found");
            Radius = 1f;
        }
        else
        {
            Radius = capsuleCollider.radius;
        }

        oldPosition = transform.position;
        OnTakeDamage.AddListener(OnTakeDamageCallback);
        OnLevelUp.AddListener(OnLevelUpCallback);
        stat.OnLevelUp.AddListener(OnStatLevelUp);
        stat.OnTakeDamage.AddListener(OnStatTakeDamage);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (stat != null)
        {
            stat.RecoverToFull();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Units.Remove(gameObject);
    }

    public static Unit Find(GameObject obj)
    {
        Unit unit;
        if (Units.TryGetValue(obj, out unit) == false)
        {
            //Debug.LogError("Unregistered unit");
        }
        return unit;
    }


    public virtual void FixedUpdateEx()
    {
        ProcessFreeze();
        ProcessKnockback();
        ProcessDamageFont();
        Animation();
    }

    /// <summary>
    /// target 위치로 이동, 회전 합니다.
    /// </summary>
    public void MoveTo(Vector3 target)
    {
        if (freezeFlag) return;
        Vector3 to = target - transform.position;
        Vector3 direction = to.normalized;
        float speed = stat.Get_FinalStat(StatType.MoveSpeed);
        transform.position += speed * direction * Time.fixedDeltaTime;
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
            case SkillKind.PoisonTrail: skill = gameObject.AddComponent<Skill_PoisonTrail>(); break;
            case SkillKind.Shuriken: skill = gameObject.AddComponent<Skill_Shuriken>(); break;
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
            case SkillKind.FrozenOrb: skill = gameObject.AddComponent<Skill_FrozenOrb>(); break;
            case SkillKind.UnstableMagicMissile: skill = gameObject.AddComponent<Skill_UnstableMagicMissile>(); break;
            case SkillKind.HeavyFireBall: skill = gameObject.AddComponent<Skill_HeavyFireBall>(); break;
            case SkillKind.Meteor: skill = gameObject.AddComponent<Skill_Meteor>(); break;
            case SkillKind.IceTornado: skill = gameObject.AddComponent<Skill_IceTornado>(); break;
            case SkillKind.PoisonTornado: skill = gameObject.AddComponent<Skill_PoisonTornado>(); break;
            case SkillKind.WindTornado: skill = gameObject.AddComponent<Skill_WindTornado>(); break;
            case SkillKind.LavaOrb: skill = gameObject.AddComponent<Skill_LavaOrb>(); break;
            case SkillKind.PoisonNova: skill = gameObject.AddComponent<Skill_PoisonNova>(); break;
            case SkillKind.CoolDown: skill = gameObject.AddComponent<Skill_CoolDown>(); break;
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
            case SkillKind.PoisonTrail: skill = gameObject.GetComponent<Skill_PoisonTrail>(); break;
            case SkillKind.Shuriken: skill = gameObject.GetComponent<Skill_Shuriken>(); break;
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
            case SkillKind.FrozenOrb: skill = gameObject.GetComponent<Skill_FrozenOrb>(); break;
            case SkillKind.UnstableMagicMissile: skill = gameObject.GetComponent<Skill_UnstableMagicMissile>(); break;
            case SkillKind.HeavyFireBall: skill = gameObject.GetComponent<Skill_HeavyFireBall>(); break;
            case SkillKind.Meteor: skill = gameObject.GetComponent<Skill_Meteor>(); break;
            case SkillKind.IceTornado: skill = gameObject.GetComponent<Skill_IceTornado>(); break;
            case SkillKind.PoisonTornado: skill = gameObject.GetComponent<Skill_PoisonTornado>(); break;
            case SkillKind.WindTornado: skill = gameObject.GetComponent<Skill_WindTornado>(); break;
            case SkillKind.LavaOrb: skill = gameObject.GetComponent<Skill_LavaOrb>(); break;
            case SkillKind.PoisonNova: skill = gameObject.GetComponent<Skill_PoisonNova>(); break;
            case SkillKind.CoolDown: skill = gameObject.GetComponent<Skill_CoolDown>(); break;
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
    /// 가지고 있는 스킬 중 랜덤으로 리턴합니다.
    /// </summary>
    public SkillKind GetRandomSkill()
    {
        int count = Skills.Count;
        int random = Random.Range(0, count);
        return Skills[random].Kind;
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
        GameObject obj = ObjectPool.Instance.Allocate("IceCubeEffect");
        ImpactV3 impact = obj.GetComponent<ImpactV3>();
        impact.Duration = time;
        impact.Target = this;
        obj.transform.position = transform.position;
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

    void ProcessDamageFont()
    {
        if(damageFontFlag) return;

        damageFontTick += Time.fixedDeltaTime;
        if(damageFontTick > damageFontDelay)
        {
            damageFontTick = 0f;
            damageFontFlag = true;
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
        // Show Damage Numbers
        if (DataManager.Instance.Settings.VisibleDamageNumbers)
        {
            if(damageFontFlag)
            {
                damageFontFlag = false;
                float dmg = damage + damageSum;
                damageSum = 0;

                // 폰트 띄워도 될 때
                GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
                UI_DamageFont font = UI_DamageFont.Find(temp.transform.GetChild(0).gameObject);
                Color fontColor = Color.white;
                Color outlineColor = Color.black;

                if (gameObject.IsPlayer())
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

                font.Init((int)dmg, fontColor, outlineColor, transform.position + (Vector3.up * 2f));
            }
            else
            {
                // 폰트 띄우면 안될 떄
                damageSum += damage; // 데미지 누적해서 다음 차례에 띄우기
                
            }
            
        }

        // Show Hurt Effect
        if (gameObject.IsPlayer())
        {
            UI_Damaged.instance.Show();
        }

        // Process Damage and Death
        float hp = stat.Get_FinalStat(StatType.Health);
        if (hp <= 0f)
        {
            Death();
            OnDead?.Invoke();
            return;
        }

        // Play Hurt Sound
        if(Sounds != null)
        {
            int index = Random.Range((int)SoundKind.Hurt01, (int)SoundKind.Hurt03 + 1);
            SoundKind soundkind = (SoundKind)index;
            Sound sound = Sounds[index];
            if(sound == null)
            {
                Debug.LogError($"[Error] Unit({name} has not Sound Data : Kind={soundkind})");
                return;
            }
            SoundManager.Instance.PlaySFXSound(sound.clip.name);
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

        if (Sounds != null)
        {
            // Play Death Sound
            int index = Random.Range((int)SoundKind.Death, (int)SoundKind.Death + 1);
            SoundKind soundkind = (SoundKind)index;
            Sound sound = Sounds[index];
            if (sound == null)
            {
                Debug.LogError($"[Error] Unit({name} has not Sound Data : Kind={soundkind})");
                return;
            }
            SoundManager.Instance.PlaySFXSound(sound.clip.name);
        }

    }

    void OnLevelUpCallback(int level)
    {

    }

    
    /// <summary>
    /// 스킬 데이터를 업데이트 합니다.
    /// </summary>
    public void UpdateSkillData()
    {
        int count = skillList.Count;
        for (int i = 0; i < count; i++)
        {
            var skill = skillList[i];
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

    public bool IsAllSkillMaxLevel()
    {
        int count = Skills.Count;
        for (int i = 0; i < count; i++)
        {
            Skill skill = Skills[i];
            if (skill.IsMaxLevel) continue;

            return false;
        }

        return true;
    }
}
