using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [SerializeField] GameObject[] prefabs;

    List<GameObject> itemList = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Drop(GameObject prefab, Vector3 position)
    {
        GameObject item = ObjectPool.Instance.Allocate(prefab.name);
        item.transform.position = position;
        itemList.Add(item);
    }
    public void Remove(GameObject target)
    {
        itemList.Remove(target);
        ObjectPool.Instance.Free(target);
    }
}
