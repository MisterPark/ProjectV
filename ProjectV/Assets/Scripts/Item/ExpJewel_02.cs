using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpJewel_02 : Item
{
    public float exp;
    
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    
    public override void Use()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Exp, exp);
        ItemManager.Instance.expJewelCount--;
    }
}
