using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Greed : Skill
{
    protected override void Awake()
    {
        base.Awake();
        //Kind = SkillKind.Greed;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Greed);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
