using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HeavyFireBall : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.HeavyFireBall;
    }
    protected override void Start()
    {
        Kind = SkillKind.HeavyFireBall;
        base.Start();
        activeInterval = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Active()
    {
        //GameObject nearest = null;
        //SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);

        //if (nearest == null)
        //{
        //    // 적이 없으면 공격 안함.
        //    return;
        //}
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        Vector3 getPoint = Random.onUnitSphere;
        getPoint = transform.position + (getPoint * 8f);
        getPoint.y = unit.skillOffsetPosition.y;
        GameObject obj = ObjectPool.Instance.Allocate("HeavyFireBall");
        SkillObject_HeavyFireBall skillObj = obj.GetComponent<SkillObject_HeavyFireBall>();
        skillObj.Initialize();
        skillObj.transform.position = transform.position + (unit.skillOffsetPosition * 1.6f);
        skillObj.team = unit.team;
        skillObj.owner = unit;
        skillObj.duration = duration;
        skillObj.damage = damage;
        skillObj.speed = speed;
        skillObj.delay = delay;
        //missile.type = MissileType.Directional;
        //missile.isPenetrate = true;
        skillObj.range = range;
        //skillObj.transform.GetChild(0).GetComponent<ParticleSystem>().startSize = range * 0.75f;
        //shape.radius = 4 * range;
        //missile.transform.GetChild(1).localScale = new Vector3(range, range, range);
        //missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);

    }
}
