using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin_02 : Item
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
        DataManager.Instance.currentGameData.gold += 10f * Player.Instance.stat.Get_FinalStat(StatType.Greed);

    }
}
