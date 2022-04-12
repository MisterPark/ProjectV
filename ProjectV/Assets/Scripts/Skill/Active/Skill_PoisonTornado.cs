using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PoisonTornado : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.PoisonTornado;
        activeInterval = 0.1f;
    }

    public override void Active()
    {
        GameObject nearest = SpawnManager.Instance.NearestMonster;
        if(nearest== null)
        {
            return;
        }
        Unit unit = Unit.Find(gameObject);
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("PoisonTornado");

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = Random.Range(0,6);
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;

        Missile missile = Missile.Find(obj);
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

        obj.transform.position = pos;
        SoundManager.Instance.PlaySFXSound("WindTornado");
    }
    void OnCollisionCallback(Vector3 pos, Unit other)
    {
    }
}
