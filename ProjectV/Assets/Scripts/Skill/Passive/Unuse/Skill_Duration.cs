using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Duration : Skill
{
    protected override void Awake()
    {
        base.Awake();
        //Kind = SkillKind.Duration;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Duration);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
