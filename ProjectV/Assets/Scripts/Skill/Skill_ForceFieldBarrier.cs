using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForceFieldBarrier : Skill
{
    GameObject obj;
    DamageObject damageObject;
    protected override void Awake()
    {
        Kind = SkillKind.ForceFieldBarrier;
    }

    protected override void Start()
    {
        Kind = SkillKind.ForceFieldBarrier;
        base.Start();

        obj = ObjectPool.Instance.Allocate("ForceFieldBarrier");
        obj.transform.position = Player.Instance.transform.position;
        obj.transform.forward = Player.Instance.transform.forward;
        obj.transform.SetParent(Player.Instance.transform);
        damageObject = obj.GetComponent<DamageObject>();
        OnLevelUpCallback(1);

        OnLevelUp.AddListener(OnLevelUpCallback);
    }

    protected override void Active()
    {
        damageObject.AttackFlag = true;
    }

    void OnLevelUpCallback(int nextLevel)
    {
        SkillData data = DataManager.Instance.skillDatas[(int)Kind].skillData;
        SkillLevel skillLevel = level.ToSkillLevel();
        SkillValue value = data.values[(int)skillLevel];
        damageObject.damage = value.damage;
    }
}
