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
            //if (MonoBehaviourEx.fixedUpdaters[i] == null || MonoBehaviourEx.fixedUpdaters[i].gameObject == null)
            //{
            //    MonoBehaviourEx.Deregister(MonoBehaviourEx.fixedUpdaters[i]);
            //    continue;
            //}
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
            //if (MonoBehaviourEx.behaviours[i] == null || MonoBehaviourEx.behaviours[i].gameObject == null)
            //{
            //    MonoBehaviourEx.Deregister(MonoBehaviourEx.behaviours[i]);
            //    continue;
            //}
            MonoBehaviourEx.updaters[i].UpdateEx();
        }


    }


}
