using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEx : MonoBehaviour
{
    public static Vector<MonoBehaviourEx> behaviours = new Vector<MonoBehaviourEx>();
    public static Vector<MonoBehaviourEx> removes = new Vector<MonoBehaviourEx>();

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Register(this);
    }


    protected virtual void OnDestroy()
    {
        Deregister(this);
    }

    public virtual void FixedUpdateEx()
    {

    }

    public virtual void UpdateEx()
    {

    }

    public static void Register(MonoBehaviourEx behaviour)
    {
        behaviours.Add(behaviour);
    }
  
    public static void Deregister(MonoBehaviourEx behaviour)
    {
        removes.Add(behaviour);
    }
}
