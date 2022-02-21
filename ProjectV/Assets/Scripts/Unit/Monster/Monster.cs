using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Unit
{
    protected override void Start()
    {
        base.Start();
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
}
