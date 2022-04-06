using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Range : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Range;
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Range);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
