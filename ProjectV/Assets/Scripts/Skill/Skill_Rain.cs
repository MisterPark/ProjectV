using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Rain : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.FireBolt;
        duration = 2.5f;
        damage = 2f;
    }
    protected override void Start()
    {
        Kind = SkillKind.FireBolt;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Active()
    {
        //GameObject nearest = null;
        //SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);

        //if (nearest == null)
        //{
        //    // ���� ������ ���� ����.
        //    return;
        //}
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        Vector3 getPoint = Random.onUnitSphere;
        getPoint = transform.position + (getPoint * 8f);
        getPoint.y = 6f;
        GameObject obj = ObjectPool.Instance.Allocate("Skill_Rain");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = getPoint;
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        //missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        
    }

}
