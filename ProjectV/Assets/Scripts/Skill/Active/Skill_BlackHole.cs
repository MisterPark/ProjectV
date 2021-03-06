using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BlackHole : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.BlackHole;
        activeInterval = 0.1f;
    }

    public override void Active()
    {
        Unit unit = Unit.Find(gameObject);
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



        Missile missile = Missile.Find(obj);
        missile.Initialize();
        
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Range = range;
        missile.Type = MissileType.Directional;
        missile.IsPenetrate = true;
        missile.IsPull = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);

        obj.transform.position = pos;
    }

    void OnCollisionCallback(Vector3 pos, Unit other )
    {
        //피격 이펙트

    }
}
