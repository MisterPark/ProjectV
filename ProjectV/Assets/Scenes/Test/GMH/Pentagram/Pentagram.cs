using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : Item
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
    

    public override void Use()
    {
        ObjectPool.Instance.Allocate("AllDelete",transform.position);
    }
}
