using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 그 패턴에서 나오는 몬스터
[System.Serializable]
public class MonstersNode
{
    [SerializeField] public List<GameObject> monsters;
}
// 분마다 나오는 몬스터(들)의 패턴종류
[System.Serializable]
public class MinuteMonsterNode
{
    [SerializeField] public List<MonstersNode> monsterPattern = new List<MonstersNode>();
    [SerializeField] public GameObject boss = null;
}

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public int MaxSpawnCount = 100;

    [SerializeField]private GameObject torchLight;

    [SerializeField] List<MinuteMonsterNode> monsterList = new List<MinuteMonsterNode>();

    List<GameObject> spawnList = new List<GameObject>();
    List<GameObject> spawnQueue = new List<GameObject>();
    GameObject nearestMonster;
    public GameObject NearestMonster { get { return nearestMonster; } }
    public List<GameObject> SpawnList{ get { return spawnList; } }
    public List<GameObject> SpawnQueue { get { return spawnQueue; } }

    private int currentMinute = -1;
    private int currentMonsterPattern = 0;
    private int currentPatternMonstersCount = 0;
    public GameObject RandomMonster 
    {
        get 
        {
            if (spawnList.Count == 0) return null;
            int index = UnityEngine.Random.Range(0, spawnList.Count);

            return spawnList[index];
        } 
    }

    float spawnDelay = 1f;
    float spawnTick = 0f;
    float freezeTick = 0f;
    float freezeTime;
    bool freezeFlag = false;

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
        ProcessFreeze();
        ProcessSpawn();
        ProcessRemove();
        PrecessSetNearestEnemy();
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

    public void FreezeAll(float time)
    {
        freezeTick = 0;
        freezeTime = time;
        freezeFlag = true;

        foreach (var monster in spawnList)
        {
            Unit unit = monster.GetComponent<Unit>();
            unit.Freeze(time);
        }
    }

    private void ProcessSpawn()
    {
        if (freezeFlag) return;
        spawnTick += Time.deltaTime;
        if (spawnTick < spawnDelay) return;
        
        spawnTick = 0f;



        int minute = ((int)DataManager.Instance.currentGameData.totalPlayTime / 60);
        if(minute >= monsterList.Count)
        {
            return;
        }

        if(currentMinute != minute)
        {
            currentMinute = minute;
            currentMonsterPattern = UnityEngine.Random.Range(0, monsterList[currentMinute].monsterPattern.Count);
            currentPatternMonstersCount = monsterList[minute].monsterPattern[currentMonsterPattern].monsters.Count;
            if(monsterList[minute].boss != null)
            {
                float angle = UnityEngine.Random.Range(-180, 180);
                float dist = 30f;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
                pos += Player.Instance.transform.position;

                Spawn(monsterList[minute].boss, pos);
            }
        }

        if (spawnList.Count >= MaxSpawnCount)
        {
            return;
        }

        int spawnCount = MaxSpawnCount - spawnList.Count;
        for (int i = 0; i < spawnCount; i++)
        {
            
            float angle = UnityEngine.Random.Range(-180, 180);
            float dist = 30f;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
            pos += Player.Instance.transform.position;

            if (UnityEngine.Random.Range(0, 100) < 5f)
            {
                Spawn(torchLight, pos);
            }
            else
            {
                int mons = UnityEngine.Random.Range(0, currentPatternMonstersCount);
                Spawn(monsterList[minute].monsterPattern[currentMonsterPattern].monsters[mons], pos);
            }
        }
    }

    private void ProcessRemove()
    {
        List<GameObject> removes = new List<GameObject>();
        foreach (var monster in spawnList)
        {
            if (monster.CompareTag("Boss")) continue;

            Vector3 to = Player.Instance.transform.position - monster.transform.position;
            float dist = to.magnitude;
            if(dist > 35f)
            {
                removes.Add(monster);
            }
        }

        foreach (var monster in removes)
        {
            Remove(monster);
        }
    }

    private void PrecessSetNearestEnemy()
    {
        spawnQueue.Clear();
        if (Player.Instance == null) return;

        spawnQueue = spawnList.OrderBy(x => x.GetDistanceFromPlayer()).ToList();

        nearestMonster = null;
        if(spawnQueue.Count > 0)
        {
             nearestMonster = spawnQueue.First();  
        }
    }

    private void ProcessFreeze()
    {
        if (freezeFlag == false) return;
        freezeTick += Time.deltaTime;
        if(freezeTick > freezeTime)
        {
            freezeTick = 0f;
            freezeFlag = false;

        }
    }

}
