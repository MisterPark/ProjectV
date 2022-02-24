using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Unit
{
    protected override void Start()
    {
        base.Start();
        type = UnitType.Monster;
        OnDead.AddListener(OnDeadCallback);
    }

    protected override void Update()
    {
        base.Update();
        MoveTo(Player.Instance.transform.position);
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.IsPlayer()) return;
        Unit target = other.gameObject.GetComponent<Unit>();
        if (target == null) return;


        Vector3 to = other.transform.position - transform.position;
        Vector3 direction = to.normalized;
        float dist = target.capsuleCollider.radius + capsuleCollider.radius;
        target.transform.position += direction * dist * Time.deltaTime;
    }

    void OnDeadCallback()
    {
        int rand = Random.Range(0, 8);
        ItemManager.Instance.Drop((ItemType)rand, transform.position);
        SpawnManager.Instance.Remove(gameObject);
    }
}
