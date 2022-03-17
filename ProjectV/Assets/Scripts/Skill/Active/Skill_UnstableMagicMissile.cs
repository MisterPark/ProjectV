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
            // ���� ������ ���� ����.
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("UnstableMagicMissile");
        UnstableMagicMissile missile = obj.GetComponent<UnstableMagicMissile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.speed = speed;
        missile.delay = delay;
        missile.type = MissileType.Other;
        missile.isPenetrate = true;
        missile.range = range;
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
