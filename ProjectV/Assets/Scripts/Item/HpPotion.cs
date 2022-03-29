using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : Item
{
    
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
        stat.Increase_FinalStat(StatType.Health, 30);
        SoundManager.Instance.PlaySFXSound("HpPotion");
    }
}
