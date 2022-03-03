using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Skill : MonoBehaviour
{
    public Sprite icon;
    public int level = 1;
    public int maxLevel = 8;
    public SkillKind Kind;
    public SkillType Type { get { return Kind.GetSkillType(); } }
    public float cooltime = 0.5f;
    public float damage;
    public int amount = 1;

    public SkillData SkillData { get { return DataManager.Instance.skillDatas[(int)Kind].skillData; } }
    public bool IsMaxLevel { get { return level == maxLevel; } }

    float tick = 0f;

    protected abstract void Active();
    protected virtual void Start()
    {
        SetValueFromSkillData(level);
    }

    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if (tick >= cooltime)
        {
            tick = 0f;
            for(int i = 0; i < amount; i++)
            {
                Active();
            }
            
        }
    }

    void SetValueFromSkillData(int level)
    {
        SkillData data = DataManager.Instance.skillDatas[(int)Kind].skillData;
        SkillLevel skillLevel = level.ToSkillLevel();
        SkillValue value = data.values[(int)skillLevel];
        amount = value.amount;
        cooltime = value.cooltime;
        damage = value.damage;
    }

    public void LevelUp()
    {
        if(level == maxLevel)
        {
            return;
        }

        level++;
        SetValueFromSkillData(level);
    }

}
