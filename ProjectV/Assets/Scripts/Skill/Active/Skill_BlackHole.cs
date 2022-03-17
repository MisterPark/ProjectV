using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BlackHole : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.BlackHole;
    }
    // Start is called before the first frame update
    protected override void Start() 
    {
        Kind = SkillKind.BlackHole;
        base.Start();

        activeInterval = 0.1f;
    }

    protected override void Active()
    {
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }

        GameObject obj = ObjectPool.Instance.Allocate("BlackHole");

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = Random.Range(0,6);
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;



        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        
        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration *Player.Instance.stat.Get_FinalStat(StatType.Duration);
        missile.damage = damage;
        missile.speed = speed * Player.Instance.stat.Get_FinalStat(StatType.Speed);
        missile.delay = delay;
        missile.range = range * Player.Instance.stat.Get_FinalStat(StatType.Range);
        missile.type = MissileType.Directional;
        missile.isPenetrate = true;
        missile.isPull = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);

        obj.transform.position = pos;
    }

    void OnCollisionCallback(Vector3 pos, Unit other )
    {
        //피격 이펙트

    }
}
