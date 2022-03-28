using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightDeath : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
