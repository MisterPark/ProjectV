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

    public List<GameObject> itemList = new List<GameObject>();
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
        itemObjectCom.MagnetFlag = false;
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
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                break;
        case ItemType.ExpJewel_Normal:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                break;
        case ItemType.ExpJewel_Small:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                break;
        case ItemType.Glass:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                break;
        case ItemType.GoldCoin_Big:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                break;
        case ItemType.GoldCoin_Normal:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                break;
        case ItemType.GoldCoin_Small:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                break;
        case ItemType.HpPotion:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                break;
        case ItemType.Magnet:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                break;
            default:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                break;
        }
        itemObject.GetComponent<ItemObject>().SetRotate(isRotate);
        //GameObject item = ObjectPool.Instance.Allocate(prefabs[(int)type].name);
        //item.transform.position = position;
        itemList.Add(itemObject);
    }
    public void Remove(GameObject target)
    {
        itemList.Remove(target);
        ObjectPool.Instance.Free(target);
    }
}
