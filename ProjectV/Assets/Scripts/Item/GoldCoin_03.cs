using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin_03 : Item
{
    protected override void Start()
    {
        base.Start();
    }
    
    public override void FixedUpdateEx()
    {
        base.FixedUpdateEx();
    }

    public override void Use()
    {
        DataManager.Instance.currentGameData.gold += 100f * Player.Instance.stat.Get_FinalStat(StatType.Greed);
        SoundManager.Instance.PlaySFXSound("ItemGet");
    }
}
