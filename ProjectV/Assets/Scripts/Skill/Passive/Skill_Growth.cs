using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Growth : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Growth;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Growth);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
