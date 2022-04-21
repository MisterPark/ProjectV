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
        int removeCount = MonoBehaviourEx.removes.Count;
        for (int i = 0; i < removeCount; i++)
        {
            MonoBehaviourEx.behaviours.Remove(MonoBehaviourEx.removes[i]);
        }
        MonoBehaviourEx.removes.Clear();

        int count = MonoBehaviourEx.behaviours.Count;
        for (int i = 0; i < count; i++)
        {
            if(MonoBehaviourEx.behaviours[i] == null || MonoBehaviourEx.behaviours[i].gameObject == null)
            {
                MonoBehaviourEx.Deregister(MonoBehaviourEx.behaviours[i]);
                continue;
            }
            if (MonoBehaviourEx.behaviours[i].gameObject.activeSelf == false) continue;
            MonoBehaviourEx.behaviours[i].FixedUpdateEx();
        }

    }
    private void Update()
    {
        int removeCount = MonoBehaviourEx.removes.Count;
        for (int i = 0; i < removeCount; i++)
        {
            MonoBehaviourEx.behaviours.Remove(MonoBehaviourEx.removes[i]);
        }
        MonoBehaviourEx.removes.Clear();

        int count = MonoBehaviourEx.behaviours.Count;
        for (int i = 0; i < count; i++)
        {
            if (MonoBehaviourEx.behaviours[i] == null || MonoBehaviourEx.behaviours[i].gameObject == null)
            {
                MonoBehaviourEx.Deregister(MonoBehaviourEx.behaviours[i]);
                continue;
            }
            if (MonoBehaviourEx.behaviours[i].gameObject.activeSelf == false) continue;
            MonoBehaviourEx.behaviours[i].UpdateEx();
        }


    }


}
