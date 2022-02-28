using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ForceFieldBarrier : Skill
{
    GameObject obj;
    DamageObject damageObject;

    bool isWaitForFrame = false;
    protected override void Start()
    {
        base.Start();
        Kind = SkillKind.ForceFieldBarrier;

        obj = ObjectPool.Instance.Allocate("ForceFieldBarrier");
        obj.transform.position = Player.Instance.transform.position;
        obj.transform.forward = Player.Instance.transform.forward;
        obj.transform.SetParent(Player.Instance.transform);

        damage = 1;
        damageObject = obj.GetComponent<DamageObject>();
        damageObject.damage = damage;

    }

    private void LateUpdate()
    {
        if(isWaitForFrame)
        {
            isWaitForFrame = false;
            damageObject.AttackFlag = false;
        }

        if(damageObject.AttackFlag)
        {
            isWaitForFrame = true;
        }
    }

    protected override void Active()
    {
        damageObject.AttackFlag = true;
    }
}
