using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Meteor : Skill
{
    
    protected override void Awake()
    {
        base.Awake();
        Kind = SkillKind.Meteor;
        activeInterval = 0.2f;
        
    }

    public override void Active()
    {
        GameObject random = SpawnManager.Instance.RandomMonster;
        if (random == null)
        {
            // 적이 없으면 공격 안함.
            return;
        }
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }

        float angle = UnityEngine.Random.Range(-180, 180);
        float dist = Random.Range(0,5);
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
        pos += Player.Instance.transform.position;

        GameObject obj = ObjectPool.Instance.Allocate("Meteor");
        Missile missile = obj.GetComponent<Missile>();
        missile.Initialize();
        missile.transform.position = pos + new Vector3(-10f, 10f, -10f); //왼쪽
        //missile.transform.position = pos + new Vector3(10f, 10f, 10f); 
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = duration;
        missile.Speed = speed;
        missile.Delay = delay;
        missile.Range = range;
        missile.Damage = damage;
        missile.Type = MissileType.Guided;
        missile.IsPenetrate = true;
        missile.SetTarget(pos);
        SoundManager.Instance.PlaySFXSound("Meteor");
    }
}
