using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Amount : Skill
{
    protected override void Awake()
    {
       //Kind = SkillKind.Amount;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Amount);
        Player.Instance.UpdateSkillData();
    }
}
