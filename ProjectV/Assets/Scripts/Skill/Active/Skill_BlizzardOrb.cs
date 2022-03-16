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
            // ���� ������ ���� ����.
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("BlizzardOrb");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.speed = speed;
        missile.delay = delay;
        missile.range = range;
        missile.type = MissileType.Directional;
        missile.isPenetrate = true;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        //Ref. BlizzardOrb Script
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
    }
}
