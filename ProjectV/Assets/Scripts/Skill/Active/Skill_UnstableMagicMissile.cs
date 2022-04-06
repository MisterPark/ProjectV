using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_UnstableMagicMissile : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.UnstableMagicMissile;
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
        SoundManager.Instance.PlaySFXSound("UnstableMagicMissile");
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        GameObject impact = ObjectPool.Instance.Allocate("UnstableMagicMissileImpact");
        impact.transform.position = other.transform.position;

    }
}
