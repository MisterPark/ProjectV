using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Magnet : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Magnet;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Magnet);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
