using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Duration : Skill
{
    protected override void Awake()
    {
        base.Awake();
        //Kind = SkillKind.Duration;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Duration);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
