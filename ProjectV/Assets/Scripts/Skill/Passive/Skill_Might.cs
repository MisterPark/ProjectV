using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Might : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.Might;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Might);
        Player.Instance.UpdateSkillData();
    }
}
