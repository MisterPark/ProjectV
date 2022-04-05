using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEx : MonoBehaviour
{
    public static List<MonoBehaviourEx> behaviours = new List<MonoBehaviourEx>();
    public static List<MonoBehaviourEx> removes = new List<MonoBehaviourEx>();
    public bool IsStart { get; private set; } = false;
    protected virtual void Awake()
    {
        Register(this);
        IsStart = true;
    }

    protected virtual void Start()
    {
        
    }


    protected virtual void OnDestroy()
    {
        Disregister(this);
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
  
    public static void Disregister(MonoBehaviourEx behaviour)
    {
        removes.Add(behaviour);
    }
}
