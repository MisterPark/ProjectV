using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMushroom : Item
{


    public override void Use()
    {
        GameObject obj = ObjectPool.Instance.Allocate("PoisonFire");

        obj.transform.position = Player.Instance.transform.position;
        obj.transform.forward = Player.Instance.transform.forward;
        Unit unit = Unit.Find(Player.Instance.gameObject);

        int minute = (int)DataManager.Instance.currentGameData.totalPlayTime / 60;

        Missile missile = Missile.Find(obj);
        missile.Initialize();
        missile.Type = MissileType.Attached;
        missile.SetTarget(Player.Instance.gameObject);
        missile.transform.position = transform.position + unit.skillOffsetPosition;
        missile.Team = unit.team;
        missile.Owner = unit;
        missile.Duration = 5 * Player.Instance.stat.Get_FinalStat(StatType.Duration);
        missile.Damage = 50 * (1+minute) * Player.Instance.stat.Get_FinalStat(StatType.Strength);
        missile.Speed = 0f;
        missile.Range = 1 * Player.Instance.stat.Get_FinalStat(StatType.Range);
        missile.Delay = 0.1f;
        missile.IsPenetrate = true;
        
    }

}
