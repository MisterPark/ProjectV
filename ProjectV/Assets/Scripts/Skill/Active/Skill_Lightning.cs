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
    public override void Active()
    {
        GameObject random = SpawnManager.Instance.RandomMonster;
        if (random == null)
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

        GameObject obj = ObjectPool.Instance.Allocate("Lightning");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = random.transform.position;
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Range = range;
        missile.SetTarget(random.transform.position);
    }
}
