using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Greed : Skill
{
    protected override void Awake()
    {
        base.Awake();
        //Kind = SkillKind.Greed;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Greed);
        Player.Instance.UpdateSkillData();
    }
}
