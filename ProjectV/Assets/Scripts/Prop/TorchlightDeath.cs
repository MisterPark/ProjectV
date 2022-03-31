using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightDeath : MonoBehaviourEx
{
    
    protected override void Start()
    {
        base.Start();
    }

    
    public override void FixedUpdateEx()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
