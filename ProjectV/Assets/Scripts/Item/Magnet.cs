using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{
    

    public override void Use()
    {
        var ItemList = ItemManager.Instance.itemList;
        var itemArray = ItemList.ToArray();
        for (int i = 0; i < itemArray.Length; i++) 
        {
            GameObject item = itemArray[i];
            ItemObject itemObject = ItemObject.Find(item);
            if(itemObject.isMagnetism)
            itemObject.MagnetFlag = true;
        }
    }
}
