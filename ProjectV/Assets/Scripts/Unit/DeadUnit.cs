using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUnit : MonoBehaviourEx, IFixedUpdater
{
    Material material;
    [SerializeField] float cutoff;
    protected override void Start()
    {
        base.Start();
        //material = GetComponentInChildren<Renderer>().material;
        cutoff = 3f;
    }

    

    protected override void OnEnable()
    {
        base.OnEnable();
        cutoff = 3f;
    }

    
    public void FixedUpdateEx()
    {
        cutoff -= Time.fixedDeltaTime;
        if(cutoff > 0)
        {
            //material.SetFloat("_CutoffHeight", cutoff);
        }
        else
        {
            ObjectPool.Instance.Free(gameObject);
        }
        
    }
}
