using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public float damage;
    public bool AttackFlag { get; set; } = false;


    void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.IsPlayer()) return;
        Unit unit = other.gameObject.GetComponent<Unit>();
        if (unit != null)
        {
            if (AttackFlag)
            {
                unit.stat.TakeDamage(damage);
            }
        }

    }
}
