using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightDeath : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
