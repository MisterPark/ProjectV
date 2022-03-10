using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public abstract class Skill : MonoBehaviour
{
    public Sprite icon;
    public int level = 1;
    public int maxLevel = 8;
    public SkillKind Kind;
    public SkillType Type { get { return Kind.GetSkillType(); } }
    public float cooltime = 0.5f;
    public float damage;
    public float duration;
    public float delay;
    public float speed;
    public float range;
    public int amount = 1;

    public UnityEvent<int> OnLevelUp = new UnityEvent<int>();

    public SkillData SkillData { get { return DataManager.Instance.skillDatas[(int)Kind].skillData; } }
    public bool IsMaxLevel { get { return level == maxLevel; } }

    Unit unit;
    float tick = 0f;

    protected abstract void Active();

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        unit = gameObject.GetComponent<Unit>();
        SetValueFromSkillData(1);
    }

    void FixedUpdate()
    {
        if (Type == SkillType.Active)
        {
            tick += Time.fixedDeltaTime;
            if (tick >= cooltime)
            {
                tick = 0f;
                for (int i = 0; i < amount; i++)
                {

                    Active();

                }
            }
        }
    }
    void SetValueFromSkillData(int level)
    {

        SkillData data = DataManager.Instance.skillDatas[(int)Kind].skillData;
        SkillLevel skillLevel = level.ToSkillLevel();
        SkillValue value = data.values[(int)skillLevel];


        int additionalAmount = Mathf.RoundToInt(unit.stat.Get_FinalStat(StatType.Amount));
        float cooltimeReduce = unit.stat.Get_FinalStat(StatType.Cooldown);
        float additionalDamage = unit.stat.Get_FinalStat(StatType.Might);
        float additionalDuration = unit.stat.Get_FinalStat(StatType.Duration);
        float additionalSpeed = unit.stat.Get_FinalStat(StatType.Speed);
        float additionalRange = unit.stat.Get_FinalStat(StatType.Area);

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
        if(level == maxLevel)
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
        float additionalDamage = unit.stat.Get_FinalStat(StatType.Might);
        float additionalDuration = unit.stat.Get_FinalStat(StatType.Duration);
        float additionalSpeed = unit.stat.Get_FinalStat(StatType.Speed);

        amount = value.amount + additionalAmount;
        cooltime = value.cooltime * cooltimeReduce;
        damage = value.damage * additionalDamage;
        duration = value.duration * additionalDuration;
        delay = value.delay;
        range = value.range;
        speed = value.speed * additionalSpeed;
    }

}
