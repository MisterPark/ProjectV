using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Strength : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.Strength;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Strength);
        Player.Instance.UpdateSkillData();
    }
}
