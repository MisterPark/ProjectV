using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpJewel_02 : Item
{
    public float exp;
    
    
    public override void Use()
    {
        Stat stat = Stat.Find(Player.Instance.gameObject);
        stat.Increase_FinalStat(StatType.Exp, exp);
        ItemManager.Instance.expJewelCount--;
    }
}
