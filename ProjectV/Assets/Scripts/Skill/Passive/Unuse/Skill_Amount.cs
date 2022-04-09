using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Amount : Skill
{
    protected override void Awake()
    {
        base.Awake();
        //Kind = SkillKind.Amount;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Amount);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
