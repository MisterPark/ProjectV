using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : MonoBehaviour
{
    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    public abstract void Use();
}
