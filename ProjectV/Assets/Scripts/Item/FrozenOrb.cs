using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenOrb : Item
{
    
    protected override void Start()
    {
        base.Start();
    }
    
    public override void FixedUpdateEx()
    {
        base.FixedUpdateEx();
    }


    public override void Use()
    {
        SpawnManager.Instance.FreezeAll(5f);
        SoundManager.Instance.PlaySFXSound("FrozenOrb");
    }
}
