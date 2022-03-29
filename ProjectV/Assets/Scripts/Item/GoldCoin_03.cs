using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin_03 : Item
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
        DataManager.Instance.currentGameData.gold += 100f * Player.Instance.stat.Get_FinalStat(StatType.Greed);
        SoundManager.Instance.PlaySFXSound("ItemGet");
    }
}
