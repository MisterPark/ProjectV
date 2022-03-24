using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_IncreaseCoin : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.IncreaseCoin;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Active()
    {
        DataManager.Instance.currentGameData.gold += 100f * Player.Instance.stat.Get_FinalStat(StatType.Greed);
    }
}
