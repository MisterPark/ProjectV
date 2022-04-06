using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lightning : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.Lightning;
        activeOnce = true;
    }
    public override void Active()
    {
        
        Unit unit = GetComponent<Unit>();
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject random = null;
            SpawnManager.Instance.SpawnQueue.Dequeue(out random);
            if (random == null) return;

            GameObject obj = ObjectPool.Instance.Allocate("Lightning");
            Missile missile = obj.GetComponent<Missile>();
            missile.Initialize();
            missile.transform.position = random.transform.position;
            missile.Team = unit.team;
            missile.Owner = unit;
            missile.Duration = duration;
            missile.Damage = damage;
            missile.Range = range;
            missile.SetTarget(random.transform.position);
            SoundManager.Instance.PlaySFXSound("Lightning");
        }

        
    }
}
