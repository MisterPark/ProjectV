using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightDeath : MonoBehaviourEx
{
    
    
    public override void FixedUpdateEx()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
