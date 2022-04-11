using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Recovery : Skill
{
    protected override void Awake()
    {
        base.Awake();
        //Kind = SkillKind.Recovery;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Recovery);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        //Kind = SkillKind.Recovery;
    }
}
