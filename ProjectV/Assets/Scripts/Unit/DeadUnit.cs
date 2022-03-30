using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUnit : MonoBehaviourEx
{
    Material material;
    [SerializeField] float cutoff;
    protected override void Start()
    {
        base.Start();
        material = GetComponentInChildren<Renderer>().material;
        cutoff = 2f;
    }

    

    private void OnEnable()
    {
        cutoff = 2f;
    }

    
    public override void FixedUpdateEx()
    {
        cutoff -= Time.fixedDeltaTime;
        if(cutoff > 0)
        {
            material.SetFloat("_CutoffHeight", cutoff);
        }
        else
        {
            ObjectPool.Instance.Free(gameObject);
        }
        
    }
}
