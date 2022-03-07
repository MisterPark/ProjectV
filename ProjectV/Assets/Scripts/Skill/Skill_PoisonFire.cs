using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PoisonFire : Skill
{

    protected override void Awake()
    {
        Kind = SkillKind.FireBolt;
    }
    protected override void Start()
    {
        base.Start();
        Kind = SkillKind.FireBolt;
    }
    protected override void Active()
    {
        throw new System.NotImplementedException();
    }
}
