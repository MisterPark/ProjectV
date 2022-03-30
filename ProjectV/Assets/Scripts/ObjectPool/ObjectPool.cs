using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviourEx
{
    private static ObjectPool _instance;

    public static ObjectPool Instance { get { return _instance; } }
    [SerializeField] List<GameObject> prefabs;
    Dictionary<string, Stack<GameObject>> _pools = new Dictionary<string, Stack<GameObject>>();
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
            _instance = this;
        //Initialize();
    }

    protected override void Start()
    {
        base.Start();
    }

    public GameObject Allocate(string key)
    {
        string _key = key.Split('(')[0];
        if (!_pools.ContainsKey(_key))
        {
            _pools.Add(_key, new Stack<GameObject>());
        }

        if (_pools[_key].Count == 0)
        {
            UpSizing(_key);
        }
        GameObject gameObject = _pools[_key].Pop();
        gameObject.name = _key;
        gameObject.SetActive(true);

        return gameObject;
    }

    public GameObject Allocate(string key, Vector3 position)
    {
        string _key = key.Split('(')[0];
        if (!_pools.ContainsKey(_key))
        {
            _pools.Add(_key, new Stack<GameObject>());
        }

        if (_pools[_key].Count == 0)
        {
            UpSizing(_key);
        }
        GameObject gameObject = _pools[_key].Pop();
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
            _pools.Add(_key, new Stack<GameObject>());
        }

        if (_pools[_key].Count == 0)
        {
            UpSizing(_key);
        }
        GameObject gameObject = _pools[_key].Pop();
        gameObject.name = _key;
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.SetActive(true);

        return gameObject;
    }
    public void Free(GameObject _gameObject)
    {
        string key = _gameObject.name.Split('(')[0];
        if (!_pools.ContainsKey(key))
        {
            _pools.Add(key, new Stack<GameObject>());
        }
        _gameObject.SetActive(false);
        _pools[key].Push(_gameObject);
    }
    private void UpSizing(string key)
    {
        if (!_pools.ContainsKey(key))
        {
            _pools.Add(key, new Stack<GameObject>());
        }

        GameObject prefab = GetPrefab(key);

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
        foreach (var prefab in prefabs)
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
            prefab.name = prefab.name.Split('(')[0];
        }
        foreach (var prefab in prefabs)
        {
            if (!_pools.ContainsKey(prefab.name))
            {
                _pools.Add(prefab.name, new Stack<GameObject>());
            }
            UpSizing(prefab.name);
        }
    }
}
