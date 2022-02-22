using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitStatData", menuName = "Scriptable Object/UnitStatData", order = int.MaxValue)]
public class UnitStatData : ScriptableObject
{
    [ArrayElementTitle("statType")]
    [SerializeField] public Stats[] stats = new Stats[(int)StatType.END];

}
