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
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Health, 30);
    }

    public override void Initialize()
    {
        
    }
}
