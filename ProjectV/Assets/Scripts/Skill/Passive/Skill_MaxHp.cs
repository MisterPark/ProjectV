using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MaxHp : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.MaxHp;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.MaxHealth);
        Player.Instance.UpdateSkillData();
    }
}
