using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : Item
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void Use()
    {
        UI_SlotMachine.instance.Show();
    }
}
