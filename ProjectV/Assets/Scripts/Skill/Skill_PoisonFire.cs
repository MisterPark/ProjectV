using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PoisonFire : Skill
{
    protected override void Start()
    {
        base.Start();
        Type = SkillType.FireBolt;
    }
    protected override void Active()
    {
        throw new System.NotImplementedException();
    }
}
