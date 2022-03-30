using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : Item
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
        // TODO : 슬롯머신 완성하면 추가
        UI_SlotMachine.instance.Show();
    }
}
