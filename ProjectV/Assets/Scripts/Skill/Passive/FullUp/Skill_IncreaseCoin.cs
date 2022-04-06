using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_IncreaseCoin : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.IncreaseCoin;
    }
    public override void Active()
    {
        DataManager.Instance.currentGameData.gold += 100f * Player.Instance.stat.Get_FinalStat(StatType.Greed);
    }

    public override void Initialize()
    {
        Kind = SkillKind.IncreaseCoin;
    }
}
