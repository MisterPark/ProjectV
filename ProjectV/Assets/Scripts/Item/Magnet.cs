using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
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
        var ItemList = ItemManager.Instance.itemList;
        foreach (var item in ItemList)
        {
            ItemObject itemObject = item.GetComponent<ItemObject>();
            if(itemObject.isMagnetism)
            itemObject.MagnetFlag = true;
        }
    }
}
