using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public int MaxSpawnCount = 100;

    [SerializeField] List<GameObject> monsterList = new List<GameObject>();

    List<GameObject> spawnList = new List<GameObject>();

    float spawnDelay = 1f;
    float spawnTick = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
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
        spawnTick += Time.deltaTime;
        if (spawnTick < spawnDelay) return;
        
        spawnTick = 0f;

        if (spawnList.Count >= MaxSpawnCount)
        {
            return;
        }

        if(monsterList.Count == 0)
        {
            return;
        }
        
        for (int i = 0; i < 20; i++)
        {
            int index = Random.Range(0, monsterList.Count - 1);
            float angle = Random.Range(-180, 180);
            float dist = 20f;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
            pos += Player.Instance.transform.position;
            Spawn(monsterList[index], pos);
        }
        

    }

    private void ProcessRemove()
    {
        List<GameObject> removes = new List<GameObject>();
        foreach (var monster in spawnList)
        {
            Vector3 to = Player.Instance.transform.position - monster.transform.position;
            float dist = to.magnitude;
            if(dist > 25f)
            {
                removes.Add(monster);
            }
        }

        foreach (var monster in removes)
        {
            Remove(monster);
        }
    }
}
