using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : MonoBehaviourEx
{
    public static Dictionary<GameObject, Item> Items = new Dictionary<GameObject, Item>();
    protected override void Start()
    {
        base.Start();
    }
    protected override void Awake()
    {
        base.Awake();
        Items.Add(gameObject, this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Items.Remove(gameObject);
    }

    public static Item Find(GameObject obj)
    {
        Item itemObj;
        if (Items.TryGetValue(obj, out itemObj) == false)
        {
            Debug.LogError("Unregistered item");
        }
        return itemObj;
    }

    public override void FixedUpdateEx()
    {

    }

    public abstract void Use();
}
