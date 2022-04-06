using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Growth : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Growth;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Growth);
        Player.Instance.UpdateSkillData();
    }

    public override void Initialize()
    {
        
    }
}
