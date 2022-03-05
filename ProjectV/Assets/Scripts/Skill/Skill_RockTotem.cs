using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RockTotem : Skill
{
    protected override void Start()
    {
        Kind = SkillKind.RockTotem;
        base.Start();
    }

    protected override void Active()
    {
        GameObject obj = ObjectPool.Instance.Allocate("RockTotem");

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = 6;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;

        DamageObject dmgobj = obj.GetComponent<DamageObject>();
        dmgobj.isPull = true;
        dmgobj.delay = delay;
        dmgobj.damage = damage;
        dmgobj.duration = duration;
        obj.transform.position = pos;

    }
}
