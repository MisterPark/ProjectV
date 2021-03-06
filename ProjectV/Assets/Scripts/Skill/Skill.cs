using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public abstract class Skill : MonoBehaviourEx, IFixedUpdater
{
    public Sprite icon;
    public int level = 1;
    public Grade grade;
    public SkillKind Kind;
    public SkillType Type { get { return Kind.GetSkillType(); } }
    public float cooltime = 0.5f;
    public float damage;
    public float duration;
    public float delay;
    public float speed;
    public float range;
    public int amount = 1;
    protected float activeInterval = 0f;
    protected float activeIntervalTick = -1f;
    protected int activeIntervalCtn = 0;
    protected bool activeOnce = false;

    public UnityEvent<int> OnLevelUp = new UnityEvent<int>();
    public UnityEvent OnDestroyed = new UnityEvent();
    public SkillData SkillData { get { return DataManager.Instance.skillDatas[(int)Kind].skillData; } }
    public int MaxLevel { get { return SkillData.maxLevel; } }
    public bool IsMaxLevel { get { return level == MaxLevel; } }
    Unit unit;
    float tick = 0f;

    public abstract void Initialize();
    public abstract void Active();

    protected override void Awake()
    {
        base.Awake();
        Initialize();
        
    }
    protected override void Start()
    {
        base.Start();
        unit = gameObject.GetComponent<Unit>();
        SetValueFromSkillData(1);
        Initialize();
    }

    public void FixedUpdateEx()
    {
        if (Type == SkillType.Active)
        {
            tick += Time.fixedDeltaTime;
            if(tick >= cooltime)
            {
                tick = 0f;
                if (activeOnce)
                {
                    Active();                
                }
                else
                {
                    activeIntervalCtn = 0;
                    activeIntervalTick = 0;
                }
            }
            if(!activeOnce && activeIntervalTick != -1f)
            {
                activeIntervalTick += Time.fixedDeltaTime;
                if(activeIntervalTick >= activeInterval)
                {
                    Active();
                    activeIntervalTick = 0f;
                    activeIntervalCtn++;
                    if(activeIntervalCtn >= amount)
                    {
                        activeIntervalTick = -1f;
                    }
                }
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnDestroyed.Invoke();
    }
    void SetValueFromSkillData(int level)
    {

        SkillData data = DataManager.Instance.skillDatas[(int)Kind].skillData;
        SkillLevel skillLevel = level.ToSkillLevel();
        SkillValue value = data.values[(int)skillLevel];


        int additionalAmount = Mathf.RoundToInt(unit.stat.Get_FinalStat(StatType.Amount));
        float cooltimeReduce = unit.stat.Get_FinalStat(StatType.Cooldown);
        float additionalDamage = unit.stat.Get_FinalStat(StatType.Strength);
        float additionalDuration = unit.stat.Get_FinalStat(StatType.Duration);
        float additionalSpeed = unit.stat.Get_FinalStat(StatType.Speed);
        float additionalRange = unit.stat.Get_FinalStat(StatType.Range);

        grade = data.grade;
        amount = value.amount + additionalAmount;
        cooltime = value.cooltime * cooltimeReduce;
        damage = value.damage * additionalDamage;
        duration = value.duration * additionalDuration;
        delay = value.delay;
        range = value.range * additionalRange;
        speed = value.speed * additionalSpeed;
        if (Type == SkillType.Passive)
        {
            Active();
        }
    }
    public void LevelUp()
    {
        if(level == MaxLevel)
        {
            return;
        }

        level++;
        SetValueFromSkillData(level);
        OnLevelUp?.Invoke(level);
    }

    public void UpdateSkillData()
    {
        SkillData data = DataManager.Instance.skillDatas[(int)Kind].skillData;
        SkillLevel skillLevel = level.ToSkillLevel();
        SkillValue value = data.values[(int)skillLevel];


        int additionalAmount = Mathf.RoundToInt(unit.stat.Get_FinalStat(StatType.Amount));
        float cooltimeReduce = unit.stat.Get_FinalStat(StatType.Cooldown);
        float additionalDamage = unit.stat.Get_FinalStat(StatType.Strength);
        float additionalDuration = unit.stat.Get_FinalStat(StatType.Duration);
        float additionalSpeed = unit.stat.Get_FinalStat(StatType.Speed);
        float additionalRange = unit.stat.Get_FinalStat(StatType.Range);

        grade = data.grade;
        amount = value.amount + additionalAmount;
        cooltime = value.cooltime * cooltimeReduce;
        damage = value.damage * additionalDamage;
        duration = value.duration * additionalDuration;
        delay = value.delay;
        range = value.range * additionalRange;
        speed = value.speed * additionalSpeed;
    }
}
