using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightDeath : MonoBehaviourEx, IFixedUpdater
{
    
    
    public void FixedUpdateEx()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
