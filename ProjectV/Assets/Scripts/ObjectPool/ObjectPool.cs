using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;

    public static ObjectPool Instance { get { return _instance; } }
    [SerializeField] List<GameObject> prefabs;
    Dictionary<string, Stack<GameObject>> _pools = new Dictionary<string, Stack<GameObject>>();
    private void Awake()
    {
        if(Instance == null)
            _instance = this;
        Initialize();
    }

    private void Start()
    {
        
    }

    public GameObject Allocate(string key) 
    {
        if(!_pools.ContainsKey(key))
        {
            Debug.LogError($"can't find key : {key}");
        }

        if (_pools[key].Count == 0)
        {
            UpSizing(key);
        }
        GameObject gameObject = _pools[key].Pop();
        gameObject.name = key;
        gameObject.SetActive(true);
        
        return gameObject;
    }

    public GameObject Allocate(string key, Vector3 position)
    {
        if (!_pools.ContainsKey(key))
        {
            Debug.LogError($"can't find key : {key}");
        }

        if (_pools[key].Count == 0)
        {
            UpSizing(key);
        }
        GameObject gameObject = _pools[key].Pop();
        gameObject.name = key;
        gameObject.transform.position = position;  
        gameObject.SetActive(true);

        return gameObject;
    }
    public void Free(GameObject _gameObject)
    {
        string key = _gameObject.name.Split('(')[0];
        if (!_pools.ContainsKey(key))
        {
            throw new System.Exception($"can't find key : {key}");            
        }
        _gameObject.SetActive(false);
        _pools[key].Push(_gameObject);
    }
    private void UpSizing(string key)
    {
        if (!_pools.ContainsKey(key))
        {
            Debug.LogError($"can't find key : {key}");           
        }

        GameObject prefab =  GetPrefab(key);
        
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(prefab, transform);
            go.SetActive(false);
            _pools[key].Push(go);
        }
    }

    private GameObject GetPrefab(string key)
    {
        GameObject _prefab = null;

        foreach(var prefab in prefabs)
        {
            if (prefab.name == key)
            {
                _prefab = prefab;
                break;
            }
        }

        return _prefab;
    }

    public void Initialize()
    {
        
        foreach (var prefab in prefabs)
        {
            if(!_pools.ContainsKey(prefab.name))
            {
                _pools.Add(prefab.name, new Stack<GameObject>());
            }
            UpSizing(prefab.name);
        }
    }

    
}
