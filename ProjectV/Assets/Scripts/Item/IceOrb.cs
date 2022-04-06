using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceOrb : Item
{
    


    public override void Use()
    {
        SpawnManager.Instance.FreezeAll(5f);
        SoundManager.Instance.PlaySFXSound("IceOrb");
    }
}
