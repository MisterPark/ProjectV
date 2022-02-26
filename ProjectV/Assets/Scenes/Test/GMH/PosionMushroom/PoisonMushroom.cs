using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMushroom : Item
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


    public override void Use()
    {
        GameObject obj = ObjectPool.Instance.Allocate("PoisonFire");
        obj.transform.position = Player.Instance.transform.position;
        obj.transform.forward = Player.Instance.transform.forward;
        obj.transform.SetParent(Player.Instance.transform);
    }

}
