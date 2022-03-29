using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : Item
{
    
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    

    public override void Use()
    {
        ObjectPool.Instance.Allocate("AllDelete",transform.position);
    }
}
