using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviourEx, IFixedUpdater
{
    public static PropManager Instance;

    public int MaxSpawnCount = 3;

    [SerializeField] GameObject[] propPrefabs;

    List<GameObject> spawnList = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void FixedUpdateEx()
    {
        ProcessSpawn();
        ProcessRemove();
    }

    public void Spawn(GameObject prefab, Vector3 position)
    {
        GameObject monster = ObjectPool.Instance.Allocate(prefab.name);
        monster.transform.position = position;
        spawnList.Add(monster);
    }

    public void Remove(GameObject target)
    {
        spawnList.Remove(target);
        ObjectPool.Instance.Free(target);
    }

    private void ProcessSpawn()
    {
        if (spawnList.Count >= MaxSpawnCount)
        {
            return;
        }

        if (propPrefabs.Length == 0)
        {
            return;
        }

        int spawnCount = MaxSpawnCount - spawnList.Count;
        for (int i = 0; i < spawnCount; i++)
        {
            int index = UnityEngine.Random.Range(0, propPrefabs.Length - 1);
            float angle = UnityEngine.Random.Range(-180, 180);
            float dist = 30f;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
            pos += Player.Instance.transform.position;
            Spawn(propPrefabs[index], pos);
        }


    }

    private void ProcessRemove()
    {
        List<GameObject> removes = new List<GameObject>();
        int count = spawnList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject monster = spawnList[i];
            Vector3 to = Player.Instance.transform.position - monster.transform.position;
            float dist = to.magnitude;
            if (dist > 35f)
            {
                removes.Add(monster);
            }
        }

        int removeCount = removes.Count;
        for (int i = 0; i < removeCount; i++)
        {
            GameObject monster = removes[i];
            Remove(monster);
        }
    }
}
