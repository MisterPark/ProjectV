using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : Unit
{
    protected override void Start()
    {
        base.Start();
        type = UnitType.Prop;
        OnDead.AddListener(OnDeadCallback);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected void OnTriggerStay(Collider other)
    {
        Unit target = other.gameObject.GetComponent<Unit>();
        if (target == null) return;

        Vector3 to = other.transform.position - transform.position;
        Vector3 direction = to.normalized;
        float dist = target.capsuleCollider.radius + capsuleCollider.radius;
        target.transform.position += direction * dist * Time.deltaTime;
    }

    void OnDeadCallback()
    {
        int rand = Random.Range((int)ItemType.HpPotion, (int)ItemType.ItemEnd);
        ItemManager.Instance.Drop((ItemType)rand, transform.position);

        PropManager.Instance.Remove(gameObject);
    }
}
