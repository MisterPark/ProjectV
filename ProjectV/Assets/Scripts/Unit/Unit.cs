using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Animator animator;
    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator Not Found");
        }
    }

    protected virtual void Update()
    {

    }

    public void MoveTo(Vector3 position)
    {

    }

}
