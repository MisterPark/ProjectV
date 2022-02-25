using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectPool.Instance.Free(gameObject);
    }
}
