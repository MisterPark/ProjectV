using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Magnet : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Magnet;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Magnet);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
