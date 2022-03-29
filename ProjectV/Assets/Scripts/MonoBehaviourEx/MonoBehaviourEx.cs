using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourEx : MonoBehaviour
{

    protected void Awake()
    {
        ObjectManager.Instance.Register(this);
    }

    protected void OnDestroy()
    {
        ObjectManager.Instance.Disregister(this);
    }

    public virtual void FixedUpdateEx()
    {

    }

    public virtual void UpdateEx()
    {

    }

    
}
