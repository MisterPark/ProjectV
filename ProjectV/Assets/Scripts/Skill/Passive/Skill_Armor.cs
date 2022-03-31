using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Armor : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Armor;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Armor);
        Player.Instance.UpdateSkillData();
    }
}
