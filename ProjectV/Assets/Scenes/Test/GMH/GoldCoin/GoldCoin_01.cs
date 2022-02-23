using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin_01 : Item
{
    [SerializeField] float rotationSpeed = 1f;
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Stat stat = other.GetComponent<Stat>();
        stat.Increase_FinalStat(StatType.Gold, 1);
    }
}
