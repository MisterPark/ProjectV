using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RecoveryHp : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.RecoveryHp;
    }
    public override void Active()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Health, 30);
    }

    public override void Initialize()
    {
        Kind = SkillKind.RecoveryHp;
    }
}
