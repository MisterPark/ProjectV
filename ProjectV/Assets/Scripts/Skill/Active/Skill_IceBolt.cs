using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_IceBolt : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.IceBolt;
    }
    protected override void Start()
    {
        Kind = SkillKind.IceBolt;
        base.Start();
        activeInterval = 0.1f;
    }
    protected override void Active()
    {
        GameObject nearest = null;
        SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);

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
        GameObject obj = ObjectPool.Instance.Allocate("IceBolt");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.range = range;
        missile.speed = speed;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        GameObject impact = ObjectPool.Instance.Allocate("IceFragmentsImpact");
        impact.transform.position = pos;

    }
}
