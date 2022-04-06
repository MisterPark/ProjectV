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

        MonoBehaviourEx[] array = MonoBehaviourEx.behaviours.ToArray();
        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            if (array[i].gameObject.activeSelf == false) continue;
            array[i].FixedUpdateEx();
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

        MonoBehaviourEx[] array = MonoBehaviourEx.behaviours.ToArray();
        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            if (array[i].gameObject.activeSelf == false) continue;
            array[i].UpdateEx();
        }


    }


}
