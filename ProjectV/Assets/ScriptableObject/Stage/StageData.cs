using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public int MaxSpawnCount = 50;
}

[System.Serializable]
public enum StageKind
{
    Stage01,
    Stage02,
    End
}

[CreateAssetMenu(fileName = "Stage Data", menuName = "Data/Stage Data"), System.Serializable]
public class StageData : ScriptableObject
{
    public StageKind kind;
    public string stageName;
    public string description;
    public Sprite icon;

    [SerializeField] public List<MinuteMonsterNode> monsterData = new List<MinuteMonsterNode>();
}
