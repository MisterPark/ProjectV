using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : Unit
{
    protected override void Awake()
    {
        base.Awake();
        stat.Init_LoadStat();
        stat.Init_FinalStat();
        type = UnitType.Prop;
        OnDead.AddListener(OnDeadCallback);
        animator = null;
    }
    protected void OnTriggerStay(Collider other)
    {
        Unit target;
        if (Unit.Units.TryGetValue(other.gameObject, out target) == false) return;
        if (target == null) return;

        Vector3 to = other.transform.position - transform.position;
        Vector3 direction = to.normalized;
        float dist = target.Radius + Radius;
        target.transform.position += direction * dist * Time.fixedDeltaTime;
    }

    void OnDeadCallback()
    {
        float luck = Player.Instance.stat.Get_FinalStat(StatType.Luck);
        //every 0.1 lucky incerase, coin drop decrease 5 (not percent)

        int random = Random.Range(0, 100 - (int)((luck - 1) * 25));
        //coin
        if(random <5)
        {
            int random2 = Random.Range((int)ItemType.Magnet,(int)ItemType.PoisonMushroom + 1);
            ItemManager.Instance.Drop((ItemType)random2, transform.position);
        }
        else if (random  < 20)
        {
            ItemManager.Instance.Drop(ItemType.HpPotion, transform.position);
        }
        else
        {
            int random2 = Random.Range(0, 10);
            if (random2 < 5)
            {
                ItemManager.Instance.Drop(ItemType.GoldCoinSmall, transform.position);
            }
            else if(random2 < 8)
            {
                ItemManager.Instance.Drop(ItemType.GoldCoinNormal, transform.position);
            }
            else
            {
                ItemManager.Instance.Drop(ItemType.GoldCoinBig, transform.position);
            }
        }
        
        
        PropManager.Instance.Remove(gameObject);
    }
}
