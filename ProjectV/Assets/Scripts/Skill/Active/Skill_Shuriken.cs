using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shuriken : Skill
{
    protected override void Start()
    {
        Kind = SkillKind.Shuriken;
        base.Start();
        activeInterval = 0.25f;
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
            Debug.LogError("��ų�� ���ָ� ��밡��.");
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

    }
    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        //�ǰ� ����Ʈ

    }
}
