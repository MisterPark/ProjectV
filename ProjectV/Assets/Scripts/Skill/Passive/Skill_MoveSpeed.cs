using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MoveSpeed : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.MoveSpeed;
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.MoveSpeed);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
