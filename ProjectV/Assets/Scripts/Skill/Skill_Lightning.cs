using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lightning : Skill
{
    protected override void Start()
    {
        Kind = SkillKind.Lightning;
        base.Start();
    }
    protected override void Active()
    {
        GameObject random = SpawnManager.Instance.RandomMonster;
        if (random == null)
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

        GameObject obj = ObjectPool.Instance.Allocate("Lightning");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = Vector3.one;
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.SetTarget(Vector3.one);
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
    }

    void OnCollisionCallback(Vector3 pos)
    {
        GameObject impact = ObjectPool.Instance.Allocate("LightningImpact");
        impact.transform.position = pos;

    }
}
