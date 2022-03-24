using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForceFieldBarrier : Skill
{
    GameObject obj;
    Missile missile;
    protected override void Awake()
    {
        Kind = SkillKind.ForceFieldBarrier;
    }

    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        ObjectPool.Instance.Free(obj);
    }

    protected override void Start()
    {
        Kind = SkillKind.ForceFieldBarrier;
        base.Start();
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }

        obj = ObjectPool.Instance.Allocate("ForceFieldBarrier");
        //obj.transform.position = Player.Instance.transform.position;
        //obj.transform.forward = Player.Instance.transform.forward;
        //obj.transform.SetParent(Player.Instance.transform);

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

        missile.Range = range;
        missile.transform.localScale = Vector3.one * range;
    }
    void OnCollisionCallback(Vector3 pos, Unit other )
    {
        //피격 이펙트

    }
}
