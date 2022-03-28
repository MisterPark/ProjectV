using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightDeath : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
