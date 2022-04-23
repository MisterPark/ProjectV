using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEx : MonoBehaviour
{
    public static List<MonoBehaviourEx> behaviours = new List<MonoBehaviourEx>();
    public static List<MonoBehaviourEx> removes = new List<MonoBehaviourEx>();
    public static List<IUpdater> updaters = new List<IUpdater>();
    public static List<IFixedUpdater> fixedUpdaters = new List<IFixedUpdater>();
    public static List<ILateUpdater> lateUpdaters = new List<ILateUpdater>();

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void OnEnable()
    {
        Register(this);
    }

    protected virtual void OnDisable()
    {
        Deregister(this);
    }


    protected virtual void OnDestroy()
    {
        
    }

    public static void Register(MonoBehaviourEx behaviour)
    {
        behaviours.Add(behaviour);
        RegisterFixedUpdater(behaviour);
        RegisterUpdater(behaviour);
        RegisterLateUpdater(behaviour);
    }
  
    public static void Deregister(MonoBehaviourEx behaviour)
    {
        removes.Add(behaviour);
    }

    public static void RegisterUpdater(MonoBehaviourEx behaviour)
    {
        IUpdater updater = behaviour as IUpdater;
        if (updater != null)
        {
            updaters.Add(updater);
        }
    }

    public static void DeregisterUpdater(MonoBehaviourEx behaviour)
    {
        IUpdater updater = behaviour as IUpdater;
        if (updater != null)
        {
            updaters.Remove(updater);
        }
    }

    public static void RegisterFixedUpdater(MonoBehaviourEx behaviour)
    {
        IFixedUpdater updater = behaviour as IFixedUpdater;
        if (updater != null)
        {
            fixedUpdaters.Add(updater);
        }
    }

    public static void DeregisterFixedUpdater(MonoBehaviourEx behaviour)
    {
        IFixedUpdater updater = behaviour as IFixedUpdater;
        if (updater != null)
        {
            fixedUpdaters.Remove(updater);
        }
    }

    public static void RegisterLateUpdater(MonoBehaviourEx behaviour)
    {
        ILateUpdater updater = behaviour as ILateUpdater;
        if (updater != null)
        {
            lateUpdaters.Add(updater);
        }
    }

    public static void DeregisterLateUpdater(MonoBehaviourEx behaviour)
    {
        ILateUpdater updater = behaviour as ILateUpdater;
        if (updater != null)
        {
            lateUpdaters.Remove(updater);
        }
    }

    public static void GarbageCollect()
    {
        int count = removes.Count;
        for (int i = 0; i < count; i++)
        {
            behaviours.Remove(removes[i]);
            DeregisterFixedUpdater(removes[i]);
            DeregisterUpdater(removes[i]);
            DeregisterLateUpdater(removes[i]);
        }
        removes.Clear();
    }
}
