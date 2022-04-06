using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : Item
{
    
    
    public override void Use()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Health, 30);
        SoundManager.Instance.PlaySFXSound("HpPotion");
    }
}
