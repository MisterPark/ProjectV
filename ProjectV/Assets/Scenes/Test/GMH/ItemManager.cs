using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType { ExpJewel_Big, ExpJewel_Normal, ExpJewel_Small,
                       Glass,
                       GoldCoin_Big, GoldCoin_Normal, GoldCoin_Small,
                       HpPotion,
                       //Level01, Level02, Level03, Level04, Level05,
                       Magnet
};
public class ItemManager : MonoBehaviour
{
    public ItemType type;
    public static ItemManager Instance;

    [SerializeField] GameObject[] prefabs;

    List<GameObject> itemList = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Drop(ItemType type, Vector3 position)
    {
        GameObject itemObject =ObjectPool.Instance.Allocate("ItemObject");
        ItemObject itemObjectCom = itemObject.GetComponent<ItemObject>();
        itemObject.transform.position = position;
        int childCount = itemObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            itemObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject item = itemObject.transform.GetChild((int)type).gameObject;
        Item itemCom = item.GetComponent<Item>();
        item.SetActive(true);
        itemObjectCom.Item = itemCom;

        bool isRotate=false;
        switch (type)
        {
        case ItemType.ExpJewel_Big:
                isRotate = true;
            break;
        case ItemType.ExpJewel_Normal:
                isRotate = true;
                break;
        case ItemType.ExpJewel_Small:
                isRotate = true;
                break;
        case ItemType.Glass:
                isRotate = true;
                break;
        case ItemType.GoldCoin_Big:
            break;
        case ItemType.GoldCoin_Normal:
            break;
        case ItemType.GoldCoin_Small:
            break;
        case ItemType.HpPotion:
            break;
        case ItemType.Magnet:
                isRotate = true;
                break;
            default:
                isRotate = false;
                break;
        }
        itemObject.GetComponent<ItemObject>().SetRotate(isRotate);
        //GameObject item = ObjectPool.Instance.Allocate(prefabs[(int)type].name);
        //item.transform.position = position;
        //itemList.Add(item);
    }
    public void Remove(GameObject target)
    {
        itemList.Remove(target);
        ObjectPool.Instance.Free(target);
    }
}
