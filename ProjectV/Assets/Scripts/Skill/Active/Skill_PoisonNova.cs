using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PoisonNova : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.PoisonNova;
        activeInterval = 0.5f;
        activeOnce = true;
    }
    public override void Active()
    {
        
        GameObject nearest = null;
        SpawnManager.Instance.SpawnQueue.Dequeue(out nearest);

        if (nearest == null)
        {
            // 적이 없으면 공격 안함.
            return;
        }
        Unit unit = Unit.Find(gameObject);
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        //float missileAmount = 12;
        for (int i = 1; i<=amount;i++)
        { 
            GameObject obj = ObjectPool.Instance.Allocate("PoisonNova");
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
            missile.IsPenetrate = false;
            float rotateIncrease =  360 / amount; //3개 120 240 360
            float rotateValue = i * rotateIncrease;
            Quaternion v3Rotation = Quaternion.Euler(0f, rotateValue, 0f);
            Vector3 v3Direction = unit.transform.forward;
            Vector3 v3RotatedDirection = v3Rotation * v3Direction;

            missile.TargetDirection = v3RotatedDirection;

            SoundManager.Instance.PlaySFXSound("PoisonNova");
        }
        //Ref. FrozenOrb Script
    }
}
