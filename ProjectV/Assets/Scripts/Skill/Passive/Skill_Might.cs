using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Might : Skill
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        Kind = SkillKind.Might;
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Active()
    {
        //플레이어스탯증가.might
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Might);
    }


}
