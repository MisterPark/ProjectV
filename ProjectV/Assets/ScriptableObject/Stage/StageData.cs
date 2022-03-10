using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
