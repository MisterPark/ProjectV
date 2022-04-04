using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : MonoBehaviourEx
{
    protected override void Start()
    {
        base.Start();
    }

    public override void FixedUpdateEx()
    {

    }

    public abstract void Use();
}
