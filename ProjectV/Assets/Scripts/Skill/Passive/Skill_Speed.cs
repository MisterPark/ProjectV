using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Speed : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.Speed;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Speed);
    }
}
