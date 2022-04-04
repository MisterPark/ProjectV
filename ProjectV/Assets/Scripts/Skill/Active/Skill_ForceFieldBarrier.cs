using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForceFieldBarrier : Skill
{
    GameObject obj;
    Missile missile;
    protected override void Awake()
    {
        base.Awake();
       
    }

    protected override void Start()
    {
        base.Start();
        Kind = SkillKind.ForceFieldBarrier;
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("��ų�� ���ָ� ��밡��.");
            return;
        }

        obj = ObjectPool.Instance.Allocate("ForceFieldBarrier");

        missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.SetTarget(unit.gameObject);

        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Range = range;
        missile.Type = MissileType.Attached;
        missile.IsPenetrate = true;
        missile.KnockbackFlag = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
        OnDestroyed.AddListener(missile.Destroy);
        OnLevelUpCallback(1);

        OnLevelUp.AddListener(OnLevelUpCallback);
        unit.OnLevelUp.AddListener(OnLevelUpCallback);
    }

    public override void Active()
    {
    }

    void OnLevelUpCallback(int nextLevel)
    {
        UpdateSkillData();

        missile.Duration = duration;
        missile.Damage = damage;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Range = range;
        missile.transform.localScale = Vector3.one * range;
    }
    void OnCollisionCallback(Vector3 pos, Unit other )
    {
        //�ǰ� ����Ʈ

    }
}
