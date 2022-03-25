using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Rain : Skill
{
    protected override void Awake()
    {
        Kind = SkillKind.Rain;
    }
    protected override void Start()
    {
        Kind = SkillKind.Rain;
        base.Start();
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
        //    // ���� ������ ���� ����.
        //    return;
        //}
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }
        Vector3 getPoint = Random.onUnitSphere;
        getPoint = transform.position + (getPoint * 8f);
        getPoint.y = unit.skillOffsetPosition.y;
        GameObject obj = ObjectPool.Instance.Allocate("Rain");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = getPoint;
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Type = MissileType.Directional;
        missile.IsPenetrate = true;
        missile.Range = range;
        var shape = missile.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
        shape.radius = 4 * range;
        missile.transform.GetChild(1).localScale = new Vector3(range, range, range);
        //missile.SetTarget(nearest.transform.position + unit.skillOffsetPosition);

        //SoundManager.Instance.masterVolumeSFX;
    }

}
