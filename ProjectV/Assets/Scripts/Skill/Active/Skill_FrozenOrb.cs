using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FrozenOrb : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.FrozenOrb;
        activeInterval = 0.25f;
    }
    public override void Active()
    {

        GameObject nearest = null;
        SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);

        if (nearest == null)
        {
            // ���� ������ ���� ����.
            return;
        }
        Unit unit = Unit.Find(gameObject);
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("FrozenOrb");
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
        missile.Type = MissileType.Directional;
        missile.IsPenetrate = true;
        missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);
        SoundManager.Instance.PlaySFXSound("FrozenOrb");
        //Ref. FrozenOrb Script
    }

    void OnCollisionCallback(Vector3 pos, Unit other)
    {
    }
}