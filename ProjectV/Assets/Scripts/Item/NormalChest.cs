using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : Item
{
    public override void Use()
    {
        // TODO : ���Ըӽ� �ϼ��ϸ� �߰�
        UI_SlotMachine.instance.Show();
    }
}
