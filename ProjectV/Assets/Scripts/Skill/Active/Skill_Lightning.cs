using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lightning : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.Lightning;
    }
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
        missile.transform.position = random.transform.position;
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.range = range;
        missile.SetTarget(random.transform.position);
    }
}
