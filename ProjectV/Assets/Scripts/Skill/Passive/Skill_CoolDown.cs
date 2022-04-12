using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_CoolDown : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.CoolDown;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Cooldown);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
