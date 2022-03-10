using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Luck : Skill
{
    protected override void Awake()
    {
       // Kind = SkillKind.Luck;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Luck);
        Player.Instance.UpdateSkillData();
    }
}
