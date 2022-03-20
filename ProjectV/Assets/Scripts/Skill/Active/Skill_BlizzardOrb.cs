using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BlizzardOrb : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.BlizzardOrb;
    }
    protected override void Start()
    {
        Kind = SkillKind.BlizzardOrb;
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
        GameObject obj = ObjectPool.Instance.Allocate("BlizzardOrb");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Range = range;
        missile.Type = MissileType.Directional;
        missile.IsPenetrate = true;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        //Ref. BlizzardOrb Script
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
    }
}
