using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireTornado : Skill
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Kind = SkillKind.FireTornado;
        base.Start();
    }

    protected override void Active()
    {
        GameObject nearest = SpawnManager.Instance.NearestMonster;
        if(nearest== null)
        {
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("FireTornado");

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = 6;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;

        DamageObject dmgobj = obj.GetComponent<DamageObject>();
        dmgobj.delay = delay;
        dmgobj.damage = damage;
        dmgobj.duration = duration;
        dmgobj.isGuided = true;
        dmgobj.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        obj.transform.position = pos;

    }
}
