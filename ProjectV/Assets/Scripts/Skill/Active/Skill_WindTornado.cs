using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_WindTornado : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.WindTornado;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        Kind = SkillKind.WindTornado;
        base.Start();
        activeInterval = 0.15f;
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
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("WindTornado");

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
        missile.IsPenetrate = false;
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
        SoundManager.Instance.PlaySFXSound("WindTornado", 0.1f);

    }
    void OnCollisionCallback(Vector3 pos, Unit other)
    {
        //�ǰ� ����Ʈ
        //GameObject impact = ObjectPool.Instance.Allocate("IceFragmentsImpact");
        //impact.transform.position = pos;
    }
}
