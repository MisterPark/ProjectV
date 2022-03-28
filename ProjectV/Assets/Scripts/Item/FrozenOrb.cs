using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenOrb : Item
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
        SpawnManager.Instance.FreezeAll(5f);
        SoundManager.Instance.PlaySFXSound("FrozenOrb");
    }
}
