using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForceFieldBarrier : Skill
{
    GameObject obj;
    protected override void Awake()
    {
        Kind = SkillKind.ForceFieldBarrier;
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
        obj.transform.position = Player.Instance.transform.position;
        obj.transform.forward = Player.Instance.transform.forward;
        obj.transform.SetParent(Player.Instance.transform);

        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();

        missile.team = unit.team;
        missile.owner = unit;
        missile.duration = duration;
        missile.damage = damage;
        missile.speed = speed;
        missile.delay = delay;
        missile.type = MissileType.Guided;
        missile.isPenetrate = true;
        missile.OnCollision.RemoveAllListeners();
        missile.OnCollision.AddListener(OnCollisionCallback);
        OnLevelUpCallback(1);

        OnLevelUp.AddListener(OnLevelUpCallback);
    }

    protected override void Active()
    {
    }

    void OnLevelUpCallback(int nextLevel)
    {
        SkillData data = DataManager.Instance.skillDatas[(int)Kind].skillData;
        SkillLevel skillLevel = level.ToSkillLevel();
        SkillValue value = data.values[(int)skillLevel];
        
    }
    void OnCollisionCallback(Vector3 pos)
    {
        //피격 이펙트

    }
}
