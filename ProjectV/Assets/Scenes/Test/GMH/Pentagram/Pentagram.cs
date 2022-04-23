using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : Item
{
    
    protected override void Start()
    {
        base.Start();
    }
    

    public override void Use()
    {
        ObjectPool.Instance.Allocate("AllDelete",transform.position);
    }
}
