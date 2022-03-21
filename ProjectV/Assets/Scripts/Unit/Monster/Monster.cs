using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Unit
{
    protected override void Start()
    {
        base.Start();
        stat.Init_LoadStat();
        stat.Init_FinalStat();
        type = UnitType.Monster;
        OnDead.AddListener(OnDeadCallback);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MoveTo(Player.Instance.transform.position);
    }

    protected void OnTriggerStay(Collider other)
    {
        
        Unit target = other.gameObject.GetComponent<Unit>();
        if (target == null) return;

        if (target.type != UnitType.Monster)
        {
            return;
        }

        Vector3 to = other.transform.position - transform.position;
        Vector3 direction = to.normalized;
        float dist = target.capsuleCollider.radius + capsuleCollider.radius;
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
            int random = Random.Range(0, 100);
            int minute = (int)DataManager.Instance.currentGameData.totalPlayTime / 60;
            int randomExp = (int)(Random.Range(8, 13) * (1 + (minute * 0.1f)));
            if (random < 90)
            {
                obj = ItemManager.Instance.Drop(ItemType.ExpJewelSmall, transform.position);
                obj.GetComponentInChildren<ExpJewel_01>().exp = randomExp;
            }
            else if (random < 99)
            {
                obj = ItemManager.Instance.Drop(ItemType.ExpJewelNormal, transform.position);
                obj.GetComponentInChildren<ExpJewel_02>().exp = randomExp * 5;
            }
            else
            {
                obj = ItemManager.Instance.Drop(ItemType.ExpJewelBig, transform.position);
                obj.GetComponentInChildren<ExpJewel_03>().exp = randomExp * 15;
            }

        }


        SpawnManager.Instance.Remove(gameObject);
    }
}
