using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_IceBolt : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.IceBolt;
        activeInterval = 0.1f;
    }
    public override void Active()
    {
        GameObject nearest = null;
        SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);

        if (nearest == null)
        {
            // 적이 없으면 공격 안함.
            return;
        }
        Unit unit = Unit.Find(gameObject);
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("IceBolt");
        Missile missile = Missile.Find(obj);
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Range = range;
        missile.Speed = speed;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
        SoundManager.Instance.PlaySFXSound("IceBolt");
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        GameObject impact = ObjectPool.Instance.Allocate("IceFragmentsImpact");
        impact.transform.position = pos;

    }
}
