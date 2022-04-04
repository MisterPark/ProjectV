using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Speed : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Speed;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Speed);
        Player.Instance.UpdateSkillData();
    }
}
