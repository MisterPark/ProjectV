using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin_01 : Item
{
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    public override void Use()
    {
        Stat stat = Player.Instance.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Gold, 1);
    }
}
