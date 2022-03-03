using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Data/Skill Data")]
public class SkillData : ScriptableObject
{
    [ArrayElementTitle("level")]
    [SerializeField] public SkillValue[] skillValues = new SkillValue[(int)SkillLevel.End];

    private void OnValidate()
    {
        for (int i = 0; i < skillValues.Length; i++)
        {
            skillValues[i].level = (SkillLevel)i;
        }
    }
}
