using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    ParticleSystem particleSystem;
    float tick = 0;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        tick = 0;
    }

    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if(particleSystem.isStopped)
        {
            ObjectPool.Instance.Free(gameObject);
        }
    }
}
