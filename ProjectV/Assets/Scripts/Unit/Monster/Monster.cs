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

        
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator != null)
        {
            animator.SetInteger("UnitType", (int)type);
        }
            
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
        Unit target;
        if (Unit.Units.TryGetValue(other.gameObject, out target) == false) return;
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
            ItemType type;
            float finalExp;
            if (random < 90)
            {
                type = ItemType.ExpJewelSmall;
                finalExp = randomExp;
            }
            else if (random < 99)
            {
                type = ItemType.ExpJewelNormal;
                finalExp = randomExp * 4;
            }
            else
            {
                type = ItemType.ExpJewelBig;
                
                finalExp = (randomExp * 10) + itemManager.expAccumulate;
                itemManager.expAccumulate = 0f;
            }
            obj = itemManager.Drop(type, transform.position);
            obj.GetComponentInChildren<ExpJewel>().exp = finalExp;
        }

        SpawnManager.Instance.Remove(gameObject);
    }
}
