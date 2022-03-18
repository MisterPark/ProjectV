using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_UnstableMagicMissile : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.UnstableMagicMissile;
    }
    protected override void Start()
    {
        Kind = SkillKind.UnstableMagicMissile;
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
        GameObject obj = ObjectPool.Instance.Allocate("UnstableMagicMissile");
        UnstableMagicMissile missile = obj.GetComponent<UnstableMagicMissile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Type = MissileType.Other;
        missile.IsPenetrate = true;
        missile.Range = range;
        missile.transform.GetChild(0).GetComponent<ParticleSystem>().startSize = range * 1.4f;
        //missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        GameObject impact = ObjectPool.Instance.Allocate("UnstableMagicMissileImpact");
        impact.transform.position = other.transform.position;

    }
}
