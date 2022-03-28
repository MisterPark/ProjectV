using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType { ExpJewelBig, ExpJewelNormal, ExpJewelSmall,
                        HpPotion,
                       GoldCoinBig, GoldCoinNormal, GoldCoinSmall,
                       //Level01, Level02, Level03, Level04, Level05,
                       Magnet,
                       Pentagram,
                       FrozenOrb,
                       PoisonMushroom,
                       NormalChest,MagicChest,RareChest,UniqueChest,LegenderyChest,
                       ItemEnd
};
public class ItemManager : MonoBehaviour
{
    public ItemType type;
    public static ItemManager Instance;

    [SerializeField] GameObject[] prefabs;
    public int expJewelMaxCount = 100;
    public int expJewelCount = 0;
    public float expAccumulate = 0;

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
    public GameObject Drop(ItemType type, Vector3 position)
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

     
        switch (type)
        {
        case ItemType.ExpJewelBig:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                itemObjectCom.isChest = false;
                break;
        case ItemType.ExpJewelNormal:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                itemObjectCom.isChest = false;
                break;
        case ItemType.ExpJewelSmall:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                itemObjectCom.isChest = false;
                break;
        case ItemType.HpPotion:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        case ItemType.GoldCoinBig:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        case ItemType.GoldCoinNormal:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        case ItemType.GoldCoinSmall:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        
        case ItemType.Magnet:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        case ItemType.Pentagram:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        case ItemType.FrozenOrb:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        case ItemType.PoisonMushroom:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        case ItemType.NormalChest:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = true;
                break;  
        case ItemType.MagicChest:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = true;
                break;
        case ItemType.RareChest:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = true;
                break;
        case ItemType.UniqueChest:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = true;
                break;
        case ItemType.LegenderyChest:
                itemObjectCom.isRotate = false;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = true;
                break;
            default:
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = false;
                itemObjectCom.isChest = false;
                break;
        }
        
        //GameObject item = ObjectPool.Instance.Allocate(prefabs[(int)type].name);
        //item.transform.position = position;
        itemList.Add(itemObject);

        if((int)type <= (int)ItemType.ExpJewelSmall)
        {
            expJewelCount++;
            if (expJewelCount > expJewelMaxCount)
            {
                AdjustTheExpJewel();
            }
        }
        return itemObject;
    }
    public void Remove(GameObject target)
    {
        itemList.Remove(target);
        ObjectPool.Instance.Free(target);
    }

    public void AdjustTheExpJewel()
    {
        GameObject expJewelObj = null;
        ExpJewel expJewel = null;
        foreach (GameObject _item in itemList)
        {
            expJewel = _item.GetComponentInChildren<ExpJewel>();
            if (expJewel != null)
            {
                expJewelObj = _item;
                break;
            }
        }

        if (expJewel == null)
        {
            return;
        }
        expJewelCount--;
        expAccumulate += expJewel.exp;

        Remove(expJewelObj);
    }
}
