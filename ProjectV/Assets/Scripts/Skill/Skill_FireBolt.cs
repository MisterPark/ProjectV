using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireBolt : Skill
{
    protected override void Start()
    {
        base.Start();
        Type = SkillType.FireBolt;
    }
    protected override void Active()
    {
        GameObject nearest = SpawnManager.Instance.NearestEnemy;
        if (nearest == null)
        {
            // 적이 없으면 공격 안함.
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("FireBolt");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.team = unit.team;
        missile.owner = unit;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
    }
}
