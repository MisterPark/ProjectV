using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{
    // Start is called before the first frame update
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
        var ItemList = ItemManager.Instance.itemList;
        foreach (var item in ItemList)
        {
            ItemObject itemObject = item.GetComponent<ItemObject>();
            if(itemObject.isMagnetism)
            itemObject.MagnetFlag = true;
        }
    }
}
