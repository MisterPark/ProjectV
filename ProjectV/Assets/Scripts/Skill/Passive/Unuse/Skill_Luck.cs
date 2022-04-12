using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Luck : Skill
{
    protected override void Awake()
    {
        base.Awake();
        // Kind = SkillKind.Luck;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Luck);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
