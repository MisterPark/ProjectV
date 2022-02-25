using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUnit : MonoBehaviour
{
    Material material;
    [SerializeField] float cutoff;
    void Start()
    {
        material = GetComponentInChildren<Renderer>().material;
        cutoff = 2f;
    }

    private void OnEnable()
    {
        cutoff = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        cutoff -= Time.deltaTime;
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
