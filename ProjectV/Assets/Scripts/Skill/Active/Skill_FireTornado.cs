using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireTornado : Skill
{
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.FireTornado;
        activeInterval = 0.1f;
    }

    public override void Active()
    {
        GameObject nearest = SpawnManager.Instance.NearestMonster;
        if(nearest== null)
        {
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("FireTornado");

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = Random.Range(0,6);
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;

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
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);

        //DamageObject dmgobj = obj.GetComponent<DamageObject>();
        //dmgobj.delay = delay;
        //dmgobj.damage = damage;
        //dmgobj.duration = duration;
        //dmgobj.isGuided = true;
        //dmgobj.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        obj.transform.position = pos;
        SoundManager.Instance.PlaySFXSound("FireTornado");
    }
    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        //피격 이펙트
        //GameObject impact = ObjectPool.Instance.Allocate("IceFragmentsImpact");
        //impact.transform.position = pos;
    }
}
