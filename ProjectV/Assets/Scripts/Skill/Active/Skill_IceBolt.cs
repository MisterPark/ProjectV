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
            // ���� ������ ���� ����.
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("IceBolt");
        Missile missile = obj.GetComponent<Missile>();
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
        SoundManager.Instance.PlaySFXSound("IceBolt", 0.1f);
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        GameObject impact = ObjectPool.Instance.Allocate("IceFragmentsImpact");
        impact.transform.position = pos;

    }
}
