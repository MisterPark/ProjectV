using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitStatData", menuName = "Data/UnitStatData")]
public class UnitStatData : ScriptableObject
{
#if UNITY_EDITOR
    [ArrayElementTitle("statType")]
#endif
    [SerializeField] public Stats[] stats = new Stats[(int)StatType.END];

}
