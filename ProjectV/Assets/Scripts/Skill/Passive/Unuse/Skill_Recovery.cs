using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Recovery : Skill
{
    protected override void Awake()
    {
        base.Awake();
        //Kind = SkillKind.Recovery;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Recovery);
        Player.Instance.UpdateSkillData();
    }
}
