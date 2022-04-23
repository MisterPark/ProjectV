using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpObject : MonoBehaviourEx, IFixedUpdater
{
    public float Default;
    public float YLimit;
    public float Speed;



    
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.position =  new Vector3(transform.position.x, Default, transform.position.z);
    }
    public void FixedUpdateEx()
    {

        if (YLimit > transform.position.y)
        {
            transform.position = transform.position + new Vector3(0f, Speed * Time.fixedDeltaTime, 0f);
        }
        else
            transform.position = new Vector3(transform.position.x, YLimit, transform.position.z);
    }
}
