using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SpawnManager : MonoBehaviourEx, IFixedUpdater
{
    public static SpawnManager Instance;

    public int MaxSpawnCount { get; set; } = 100;

    [SerializeField]private GameObject torchLight;

    [SerializeField] List<StageData> stageData = new List<StageData>();
    [SerializeField] List<MinuteMonsterNode> stageMonsterData;

    Vector<GameObject> spawnList = new Vector<GameObject>();
    Vector<GameObject> spawnQueue = new Vector<GameObject>();
    GameObject nearestMonster;
    public GameObject NearestMonster { get { return nearestMonster; } }
    public Vector<GameObject> SpawnList{ get { return spawnList; } }
    public Vector<GameObject> SpawnQueue { get { return spawnQueue; } }

    private int currentMinute = -1;
    private int currentMonsterPattern = 0;
    private int currentPatternMonstersCount = 0;
    [SerializeField] private float spawnTimeInterval = 0.1f;
    [SerializeField] private float spawnTimeTick = 0f;

    private bool pauseFlag = false;
    public bool Pause { get {return pauseFlag;} set { pauseFlag = value; } }
    public GameObject RandomMonster 
    {
        get 
        {
            if (spawnList.Count == 0) return null;
            int index = UnityEngine.Random.Range(0, spawnList.Count);

            return spawnList[index];
        } 
    }

    float freezeTick = 0f;
    float freezeTime;
    bool freezeFlag = false;

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        stageMonsterData = stageData[0].monsterData;
    }
    public void FixedUpdateEx()
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

    public void Erase(GameObject target)
    {
        spawnList.Remove(target);
    }

    public void FreezeAll(float time)
    {
        freezeTick = 0;
        freezeTime = time;
        freezeFlag = true;

        GameObject[] spawnArray = spawnList.ToArray();
        for (int i = 0; i < spawnArray.Length; i++)
        {
            GameObject monster = spawnArray[i];
            Unit unit = Unit.Find(monster);
            unit.Freeze(time);
        }
    }

    private void ProcessSpawn()
    {
        if (Pause) return;
        if (freezeFlag) return;


        int minute = ((int)DataManager.Instance.currentGameData.totalPlayTime / 60);
        if(minute >= stageMonsterData.Count)
        {
            return; // 게임클리어 시간을 지났을때, 생성 막음.
        }

        if(currentMinute != minute)
        {
            currentMinute = minute;
            currentMonsterPattern = UnityEngine.Random.Range(0, stageMonsterData[currentMinute].monsterPattern.Count);
            currentPatternMonstersCount = stageMonsterData[minute].monsterPattern[currentMonsterPattern].monsters.Count;
            MaxSpawnCount = stageMonsterData[minute].MaxSpawnCount;
            if(stageMonsterData[minute].boss != null)
            {
                float angle = UnityEngine.Random.Range(-180, 180);
                float dist = 30f;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
                pos += Player.Instance.transform.position;

                Spawn(stageMonsterData[minute].boss, pos);
            }
        }

        if (spawnList.Count >= MaxSpawnCount)
        {
            return;
        }

        spawnTimeTick += Time.fixedDeltaTime;
        if(spawnTimeTick >= spawnTimeInterval)
        {
            spawnTimeTick -= spawnTimeInterval;
            float angle = UnityEngine.Random.Range(-180, 180);
            float dist = 25f;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * dist;
            pos += Player.Instance.transform.position;

            if (UnityEngine.Random.Range(0, 100) < 5f)
            {
                Spawn(torchLight, pos); // 5% 확률로 보너스 몬스터 생성
            }
            else
            {
                int mons = UnityEngine.Random.Range(0, currentPatternMonstersCount);
                Spawn(stageMonsterData[minute].monsterPattern[currentMonsterPattern].monsters[mons], pos);
            }
        }

    }

    private void ProcessRemove()
    {
        Vector<GameObject> erases = new Vector<GameObject>();
        Vector<GameObject> removes = new Vector<GameObject>();
        GameObject[] spawnArray = spawnList.ToArray();
        for (int i = 0; i < spawnArray.Length; i++)
        {
            GameObject monster = spawnArray[i];
            if (monster == null || monster.activeSelf == false)
            {
                erases.Add(monster);
                continue;
            }
            if (monster.CompareTag("Boss")) continue;

            Vector3 to = Player.Instance.transform.position - monster.transform.position;
            float dist = to.magnitude;
            if(dist > 30f)
            {
                removes.Add(monster);
            }
        }

        for (int j = 0; j < erases.Count; j++)
        {
            GameObject monster = erases[j];
            Erase(monster);
        }
        erases.Clear();

        for (int j = 0; j < removes.Count; j++)
        {
            GameObject monster = removes[j];
            Remove(monster);
        }
        removes.Clear();
    }

    

    private void PrecessSetNearestEnemy()
    {
        spawnQueue.Clear();
        if (Player.Instance == null) return;

        //spawnQueue = spawnList.OrderBy(x => x.GetDistanceFromPlayer());
        spawnList.Sort(x => x.GetDistanceFromPlayer());
        spawnQueue = new Vector<GameObject>(spawnList);

        nearestMonster = null;
        if(spawnQueue.Count > 0)
        {
             nearestMonster = spawnQueue.First();  
        }
    }

    private void ProcessFreeze()
    {
        if (freezeFlag == false) return;
        freezeTick += Time.fixedDeltaTime;
        if(freezeTick > freezeTime)
        {
            freezeTick = 0f;
            freezeFlag = false;

        }
    }

}
