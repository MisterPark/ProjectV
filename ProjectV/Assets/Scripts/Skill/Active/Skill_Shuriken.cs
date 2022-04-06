using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shuriken : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.Shuriken;
        activeInterval = 0.25f;
    }
    public override void Active()
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
        GameObject obj = ObjectPool.Instance.Allocate("Shuriken");

        //float angle = UnityEngine.Random.Range(-180, 180);
        //float dist = 6;
        //Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;


        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Range = range;
        missile.Type = MissileType.Guided;
        missile.IsPenetrate = true;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        missile.IsRotate = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);

        obj.transform.position = Player.Instance.transform.position + unit.skillOffsetPosition;

        SoundManager.Instance.PlaySFXSound("Shuriken");
    }
    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        //피격 이펙트

    }
}
