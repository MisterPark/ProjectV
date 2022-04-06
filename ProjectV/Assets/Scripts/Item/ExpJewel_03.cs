using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpJewel_03 : Item
{
    public float exp;
    
    
    public override void Use()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Exp, exp);
        ItemManager.Instance.expJewelCount--;
    }
}
