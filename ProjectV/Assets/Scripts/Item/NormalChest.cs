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
        // TODO : ���Ըӽ� �ϼ��ϸ� �߰�
        UI_SlotMachine.instance.Show();
    }
}
