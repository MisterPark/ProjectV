using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Unit
{
    protected override void Awake()
    {
        base.Awake();
        stat.Init_LoadStat();
        stat.Init_FinalStat();
        type = UnitType.Monster;
        OnDead.AddListener(OnDeadCallback);
    }
    protected override void Start()
    {
        base.Start();
    }

    public override void FixedUpdateEx()
    {
        base.FixedUpdateEx();
        MoveTo(Player.Instance.transform.position);
    }

    protected void OnTriggerStay(Collider other)
    {
        Unit target = other.gameObject.GetComponent<Unit>();
        if (target == null) return;
        if (target.type != UnitType.Monster) return;

        Vector3 to = other.transform.position - transform.position;
        Vector3 direction = to.normalized;
        float dist = target.Radius + Radius;
        target.transform.position += direction * dist * Time.deltaTime;
    }

    void OnDeadCallback()
    {
        DataManager.Instance.currentGameData.killCount += 1;
        if (CompareTag("Boss"))
        {
            ItemManager.Instance.Drop(ItemType.NormalChest, transform.position);
        }
        else
        {
            GameObject obj;
            ItemManager itemManager = ItemManager.Instance;
            int random = Random.Range(0, 100);
            int minute = (int)DataManager.Instance.currentGameData.totalPlayTime / 60;
            float randomExp = Random.Range(8, 13) * (1 + (minute * 0.2f)); // 경험치량
            if (random < 90)
            {
                obj = itemManager.Drop(ItemType.ExpJewelSmall, transform.position);
                obj.GetComponentInChildren<ExpJewel>().exp = randomExp;
            }
            else if (random < 99)
            {
                obj = itemManager.Drop(ItemType.ExpJewelNormal, transform.position);
                obj.GetComponentInChildren<ExpJewel>().exp = randomExp * 4;
            }
            else
            {
                obj = itemManager.Drop(ItemType.ExpJewelBig, transform.position);
                obj.GetComponentInChildren<ExpJewel>().exp = (randomExp * 10) + itemManager.expAccumulate;
                itemManager.expAccumulate = 0f;
            }
        }

        SpawnManager.Instance.Remove(gameObject);
    }
}
