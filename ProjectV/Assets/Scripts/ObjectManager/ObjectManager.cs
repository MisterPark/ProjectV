using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public static ObjectManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void FixedUpdate()
    {
        MonoBehaviourEx.GarbageCollect();

        int count = MonoBehaviourEx.fixedUpdaters.Count;
        for (int i = 0; i < count; i++)
        {
            MonoBehaviourEx.fixedUpdaters[i].FixedUpdateEx();
        }

        Debug.Log(count);
    }
    private void Update()
    {
        MonoBehaviourEx.GarbageCollect();

        int count = MonoBehaviourEx.updaters.Count;
        for (int i = 0; i < count; i++)
        {
            MonoBehaviourEx.updaters[i].UpdateEx();
        }


    }


}
