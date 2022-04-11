using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HeavyFireBall : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.HeavyFireBall;
        activeInterval = 0.1f;
    }


    public override void Active()
    {
        Unit unit = Unit.Find(gameObject);
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        Vector3 getPoint = Random.onUnitSphere;
        getPoint = transform.position + (getPoint * 8f);
        getPoint.y = unit.skillOffsetPosition.y;
        GameObject obj = ObjectPool.Instance.Allocate("HeavyFireBall");
        SkillObject_HeavyFireBall skillObj = obj.GetComponent<SkillObject_HeavyFireBall>();
        skillObj.Initialize();
        skillObj.transform.position = transform.position + (unit.skillOffsetPosition * 2f);
        skillObj.team = unit.team;
        skillObj.owner = unit;
        skillObj.duration = duration;
        skillObj.damage = damage;
        skillObj.speed = speed;
        skillObj.delay = delay;
        skillObj.range = range;
        SoundManager.Instance.PlaySFXSound("HeavyFireBall");
    }
}
