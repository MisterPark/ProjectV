using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin_03 : Item
{
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
        stat.StatIncreases(StatType.Gold, 100);
    }
}
