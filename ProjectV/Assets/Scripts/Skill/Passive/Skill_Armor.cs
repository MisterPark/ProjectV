using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Armor : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Armor;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Armor);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
