using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenOrb : Item
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
        SpawnManager.Instance.FreezeAll(5f);
        SoundManager.Instance.PlaySFXSound("FrozenOrb");
    }
}
