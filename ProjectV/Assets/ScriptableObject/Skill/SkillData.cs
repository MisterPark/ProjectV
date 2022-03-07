using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Data/Skill Data"), System.Serializable]
public class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public SkillKind kind;
    public SkillType type;
    public Grade grade;
#if UNITY_EDITOR
    [ArrayElementTitle("level")]
#endif
    public SkillValue[] values = new SkillValue[(int)SkillLevel.End];

    private void OnValidate()
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i].level = (SkillLevel)i;
        }
    }
}
[System.Serializable]
public class SkillDataElement
{
    public SkillKind kind;
    public SkillData skillData;
}

