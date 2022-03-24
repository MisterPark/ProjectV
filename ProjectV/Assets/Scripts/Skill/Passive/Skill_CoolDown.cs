using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_CoolDown : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.CoolDown;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Cooldown);
        Player.Instance.UpdateSkillData();
    }
}
