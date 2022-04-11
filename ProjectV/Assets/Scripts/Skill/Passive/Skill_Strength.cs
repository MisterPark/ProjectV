using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Strength : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Strength;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Strength);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
