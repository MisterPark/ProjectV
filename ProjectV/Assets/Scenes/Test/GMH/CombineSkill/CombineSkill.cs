using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combine Skill", menuName = "Data/Combine Skill Data"), System.Serializable]

public class CombineSkill : ScriptableObject
{
    
        public SkillKind materialA;
        public SkillKind materialB;

        public SkillKind combinedSkill;
    
}

[System.Serializable]
public class CombineSkillDataElement
{
    public SkillKind combinedSkill;
    public CombineSkill combineSkillData;
}
