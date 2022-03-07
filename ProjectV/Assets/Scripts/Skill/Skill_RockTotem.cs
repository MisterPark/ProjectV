using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RockTotem : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.RockTotem;
    }
    protected override void Start()
    {
        Kind = SkillKind.RockTotem;
        base.Start();
    }

    protected override void Active()
    {
        GameObject obj = ObjectPool.Instance.Allocate("RockTotem");

        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = 6;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;


        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.speed = speed;
        missile.delay = delay;
        missile.type = MissileType.Guided;
        missile.isPenetrate = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
        obj.transform.position = pos;
    }
    void OnCollisionCallback(Vector3 pos)
    {
        //피격 이펙트

    }
}
