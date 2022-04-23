using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviourEx
{
    private static ObjectPool _instance;

    public static ObjectPool Instance { get { return _instance; } }
    [SerializeField] GameObject[] prefabs;
    Dictionary<string, List<GameObject>> _pools = new Dictionary<string, List<GameObject>>();
    protected override void Awake()
    {
        base.Awake();
        if (_instance == null)
        {
            _instance = this;
        }
        //Initialize();
    }
    public void Initialize()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            string key = prefab.name.Split('(')[0];
            prefab.name = key;
            if (!_pools.ContainsKey(key))
            {
                _pools.Add(key, new List<GameObject>());
            }
            UpSizing(key);
        }
    }
    public GameObject Allocate(string key)
    {
        string _key = key.Split('(')[0];
        if (!_pools.ContainsKey(_key))
        {
            _pools.Add(_key, new List<GameObject>());
        }

        if (_pools[_key].Count == 0)
        {
            UpSizing(_key);
        }
        GameObject gameObject = _pools[_key].PopFront();
        gameObject.name = _key;
        gameObject.SetActive(true);

        return gameObject;
    }
    public GameObject Allocate(string key, Vector3 position)
    {
        string _key = key.Split('(')[0];
        if (!_pools.ContainsKey(_key))
        {
            _pools.Add(_key, new List<GameObject>());
        }

        if (_pools[_key].Count == 0)
        {
            UpSizing(_key);
        }
        GameObject gameObject = _pools[_key].PopFront();
        gameObject.name = _key;
        gameObject.transform.position = position;
        gameObject.SetActive(true);

        return gameObject;
    }
    public GameObject Allocate(string key, Vector3 position,Quaternion rotation)
    {
        string _key = key.Split('(')[0];
        if (!_pools.ContainsKey(_key))
        {
            _pools.Add(_key, new List<GameObject>());
        }

        if (_pools[_key].Count == 0)
        {
            UpSizing(_key);
        }
        GameObject gameObject = _pools[_key].PopFront();
        gameObject.name = _key;
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);

        return gameObject;
    }
    public void Free(GameObject _gameObject)
    {
        string key = _gameObject.name.Split('(')[0];
        if (!_pools.ContainsKey(key))
        {
            _pools.Add(key, new List<GameObject>());
        }
        _gameObject.SetActive(false);
        _pools[key].PushFront(_gameObject);
    }
    private void UpSizing(string key)
    {
        if (!_pools.ContainsKey(key))
        {
            _pools.Add(key, new List<GameObject>());
        }

        GameObject prefab = GetPrefab(key);

        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(prefab, transform);
            go.SetActive(false);
            _pools[key].Add(go);
        }
    }
    private GameObject GetPrefab(string key)
    {
        GameObject _prefab = null;
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            if (prefab.name == key)
            {
                _prefab = prefab;
                break;
            }
        }
        return _prefab;
    }
    
}
