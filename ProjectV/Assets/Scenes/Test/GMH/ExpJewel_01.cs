using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpJewel_01 : Item
{
    // Start is called before the first frame update
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
    }
}
