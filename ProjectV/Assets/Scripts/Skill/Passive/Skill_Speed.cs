using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Speed : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Speed;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Speed);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
