using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Meteor : Skill
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        Kind = SkillKind.Meteor;
    }
    protected override void Start()
    {
        Kind = SkillKind.Meteor;
        base.Start();
        activeInterval = 0.1f;
    }

    protected override void Active()
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

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = Random.Range(0,6);
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;

        GameObject obj = ObjectPool.Instance.Allocate("Meteor");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = pos + new Vector3(-10f, 10f, -10f); //왼쪽
        //missile.transform.position = pos + new Vector3(10f, 10f, 10f); 
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.speed = speed;
        missile.delay = delay;
        missile.range = range;
        missile.damage = damage;
        missile.type = MissileType.Guided;
        missile.isPenetrate = true;
        missile.SetTarget(pos);
    }
}
