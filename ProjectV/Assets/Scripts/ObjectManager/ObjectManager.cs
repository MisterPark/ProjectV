using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public static ObjectManager Instance { get { return instance; } }
    List<MonoBehaviourEx> behaviours = new List<MonoBehaviourEx>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void FixedUpdate()
    {
        foreach(var behaviour in behaviours)
        {
            behaviour.FixedUpdateEx();
        }
    }
    private void Update()
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.UpdateEx();
        }
    }

    public void Register(MonoBehaviourEx behaviour)
    {
        behaviours.Add(behaviour);
    }

    public void Disregister(MonoBehaviourEx behaviour)
    {
        behaviours.Remove(behaviour);
    }

   
}
