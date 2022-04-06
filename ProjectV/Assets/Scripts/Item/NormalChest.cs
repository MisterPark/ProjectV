using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : Item
{
    public override void Use()
    {
        // TODO : 슬롯머신 완성하면 추가
        UI_SlotMachine.instance.Show();
    }
}
