using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireTornado : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.FireTornado;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        Kind = SkillKind.FireTornado;
        base.Start();
    }

    protected override void Active()
    {
        GameObject nearest = SpawnManager.Instance.NearestMonster;
        if(nearest== null)
        {
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("FireTornado");

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = 6;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;

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
        missile.radiusSize = range * 3.6f;
        missile.type = MissileType.Guided;
        missile.isPenetrate = true;
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

    }
    void OnCollisionCallback(Vector3 pos)
    {
        //�ǰ� ����Ʈ
        //GameObject impact = ObjectPool.Instance.Allocate("IceFragmentsImpact");
        //impact.transform.position = pos;
    }
}
