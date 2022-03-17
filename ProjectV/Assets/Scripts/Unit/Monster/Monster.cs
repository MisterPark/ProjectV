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
        int itemType = (int)ItemType.ExpJewel_Small;
        int playTime = (int)DataManager.Instance.currentGameData.totalPlayTime;
        int minute = playTime / 60;

        // TODO : 몬스터에 따른 드랍 아이템이 생기면 바꿔야 함
        if (minute > 20)
        {
            itemType = (int)ItemType.ExpJewel_Big;
        }
        else if (minute > 10)
        {
            itemType = (int)ItemType.ExpJewel_Normal;
        }

        ItemManager.Instance.Drop((ItemType)itemType, transform.position);


        SpawnManager.Instance.Remove(gameObject);
    }
}
