using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ShurikenAttack : Skill
{
    protected override void Start()
    {
        Kind = SkillKind.ShurikenAttack;
        base.Start();
    }

    protected override void Active()
    {

        //큐 빼기 말고 랜덤으로 바꿔야함 
        GameObject nearest = null;
        SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("ShurikenAttack");

        //float angle = UnityEngine.Random.Range(-180, 180);
        //float dist = 6;
        //Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        

        DamageObject dmgobj = obj.GetComponent<DamageObject>();
        dmgobj.delay = delay;
        dmgobj.damage = damage;
        dmgobj.duration = duration;
        dmgobj.speed = speed;
        dmgobj.isGuided = true;
        dmgobj.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        obj.transform.position = Player.Instance.transform.position + unit.skillOffsetPosition;

    }
}
