using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin_01 : Item
{
    
    public override void Use()
    {
        DataManager.Instance.currentGameData.gold += 1f * Player.Instance.stat.Get_FinalStat(StatType.Greed);
        SoundManager.Instance.PlaySFXSound("ItemGet");
    }
}
