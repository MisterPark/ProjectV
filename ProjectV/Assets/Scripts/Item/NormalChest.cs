using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : Item
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
        // TODO : ���Ըӽ� �ϼ��ϸ� �߰�
        UI_SlotMachine.instance.Show();
    }
}
