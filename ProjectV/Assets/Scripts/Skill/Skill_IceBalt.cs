using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_IceBalt : Skill
{
    protected override void Start()
    {
        base.Start();
        Type = SkillType.IceBalt;
    }
    protected override void Active()
    {
        GameObject nearest = SpawnManager.Instance.NearestEnemy;
        if(nearest == null)
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
        GameObject obj = ObjectPool.Instance.Allocate("IceBalt");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.team = unit.team;
        missile.owner = unit;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
    }
}
