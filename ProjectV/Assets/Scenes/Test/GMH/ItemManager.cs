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
                       IceOrb,
                       PoisonMushroom,
                       NormalChest,MagicChest,RareChest,UniqueChest,LegenderyChest,
                       ItemEnd
};
public class ItemManager : MonoBehaviourEx, IFixedUpdater
{
    public ItemType type;
    public static ItemManager Instance;

    [SerializeField] GameObject[] prefabs;
    [SerializeField] private int expJewelMaxCount = 100;
    public int expJewelCount = 0;
    public float expAccumulate = 0;
    [SerializeField] private float farExpJewelDeleteTime = 5f;
    [SerializeField] private float farExpJewelDeleteTick = 0f;
    [SerializeField] private float farExpJewelDeleteDistance = 25f;

    public List<GameObject> itemList = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void FixedUpdateEx()
    {
        
        FarExpJewelDelete();
    }

    public GameObject Drop(ItemType type, Vector3 position)
    {
        GameObject itemObject =ObjectPool.Instance.Allocate("ItemObject");
        ItemObject itemObjectCom = ItemObject.Find(itemObject);
        if (itemObjectCom == null) return null;
        itemObject.transform.position = position;
        itemObjectCom.MagnetFlag = false;
        int childCount = itemObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            itemObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject item = itemObject.transform.GetChild((int)type).gameObject;
        Item itemCom = Item.Find(item);
        if(itemCom == null) return null;
        item.SetActive(true);
        itemObjectCom.Item = itemCom;
        //itemObjectCom.LifeTime = 0;
        //itemObjectCom.SinWaveFlag = false;

        switch (type)
        {
        case ItemType.ExpJewelBig:
                //item.gameObject.transform.localPosition = Vector3.zero;
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                itemObjectCom.isChest = false;
                //itemObjectCom.SinWaveFlag = true;
                break;
        case ItemType.ExpJewelNormal:
                //item.gameObject.transform.localPosition = Vector3.zero;
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                itemObjectCom.isChest = false;
                //itemObjectCom.SinWaveFlag = true;
                break;
        case ItemType.ExpJewelSmall:
                //item.gameObject.transform.localPosition = Vector3.zero;
                itemObjectCom.isRotate = true;
                itemObjectCom.isMagnetism = true;
                itemObjectCom.isChest = false;
                //itemObjectCom.SinWaveFlag = true;
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
        case ItemType.IceOrb:
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
        var items = itemList.ToArray();
        for (int i = 0; i < items.Length; i++)
        {
            GameObject _item = items[i];
            expJewel = _item.GetComponentInChildren<ExpJewel>(); // 가장 오래있는 경험치 보석 찾기
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

    public void FarExpJewelDelete()
    {
        farExpJewelDeleteTick += Time.fixedDeltaTime;
        List<GameObject> deleteList = new List<GameObject>();
        if (farExpJewelDeleteTick >= farExpJewelDeleteTime)
        {
            farExpJewelDeleteTick = 0f;
            Vector3 playerPos = Player.Instance.transform.position;
            var items = itemList.ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                GameObject _item = items[i];
                ExpJewel expJewel = _item.GetComponentInChildren<ExpJewel>();
                if (expJewel == null)
                {
                    continue;
                }
                float distance = Vector3.Distance(playerPos, _item.transform.position);
                if (farExpJewelDeleteDistance <= distance)
                {
                    expJewelCount--;
                    expAccumulate += expJewel.exp;

                    deleteList.Add(_item);
                }
            }

            GameObject[] deleteArray = deleteList.ToArray();
            for (int i = 0; i < deleteArray.Length; i++)
            {
                GameObject _item = deleteArray[i];
                Remove(_item);
            }
        }
    }
}
