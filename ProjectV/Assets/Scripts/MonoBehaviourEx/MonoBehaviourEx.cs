using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEx : MonoBehaviour
{
    public static List<MonoBehaviourEx> behaviours = new List<MonoBehaviourEx>();
    public static List<MonoBehaviourEx> removes = new List<MonoBehaviourEx>();

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
