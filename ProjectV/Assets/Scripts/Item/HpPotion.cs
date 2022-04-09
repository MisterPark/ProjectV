using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : Item
{
    
    
    public override void Use()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Health, 30);
        SoundManager.Instance.PlaySFXSound("HpPotion");
    }
}
