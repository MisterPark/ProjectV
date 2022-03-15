using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_LavaOrb : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.LavaOrb;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        Kind = SkillKind.LavaOrb;
        base.Start();
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
        GameObject obj = ObjectPool.Instance.Allocate("LavaOrb");

        //float angle = UnityEngine.Random.Range(-180, 180);
        //float dist = Random.Range(0, 6);
        Vector3 pos = Player.Instance.transform.position;

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
        missile.type = MissileType.Other;
        missile.isPenetrate = true;
        missile.SetTarget(unit.transform.position);
        missile.OnCollision.RemoveAllListeners();

        obj.transform.position = pos;

    }
}
