using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ShurikenAttack : Skill
{
    protected override void Start()
    {
        Kind = SkillKind.ShurikenAttack;
        base.Start();
        activeInterval = 0.1f;
    }

    protected override void Active()
    {

        GameObject nearest = SpawnManager.Instance.NearestMonster;
        if (nearest == null)
        {
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("ShurikenAttack");

        //float angle = UnityEngine.Random.Range(-180, 180);
        //float dist = 6;
        //Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;


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
        missile.type = MissileType.Guided;
        missile.isPenetrate = true;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        missile.isRotate = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);

        obj.transform.position = Player.Instance.transform.position + unit.skillOffsetPosition;

    }
    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        //피격 이펙트

    }
}
