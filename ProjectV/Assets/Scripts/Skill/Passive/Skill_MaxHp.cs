using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MaxHp : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.MaxHp;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.MaxHealth);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
