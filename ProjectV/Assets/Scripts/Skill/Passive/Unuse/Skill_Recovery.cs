using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Recovery : Skill
{
    protected override void Awake()
    {
        //Kind = SkillKind.Recovery;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Recovery);
        Player.Instance.UpdateSkillData();
    }
}
