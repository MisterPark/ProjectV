using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_LavaOrb : Skill
{
    public override void Initialize()
    {
        Kind = SkillKind.LavaOrb;
        activeInterval = 0.4f;
    }

    public override void Active()
    {
        GameObject nearest = SpawnManager.Instance.NearestMonster;
        if (nearest == null)
        {
            return;
        }
        Unit unit = Unit.Find(gameObject);
        if (unit == null)
        {
            Debug.LogError("스킬을 유닛만 사용가능.");
            return;
        }
        GameObject obj = ObjectPool.Instance.Allocate("LavaOrb");

        //float angle = UnityEngine.Random.Range(-180, 180);
        //float dist = Random.Range(0, 6);
        Vector3 pos = Player.Instance.transform.position + unit.skillOffsetPosition + new Vector3(0f,0f,1f);

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
        missile.Type = MissileType.Other;
        missile.IsPenetrate = true;
        missile.SetTarget(unit.transform.position);
        missile.OnCollision.RemoveAllListeners();

        obj.transform.position = pos;

        SoundManager.Instance.PlaySFXSound("LavaOrb");
    }
}
