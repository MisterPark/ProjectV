using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Magnet : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.Magnet;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Magnet);
        Player.Instance.UpdateSkillData();
    }
}