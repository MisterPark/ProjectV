using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combine Skill", menuName = "Data/Combine Skill Data"), System.Serializable]

public class CombineSkill : ScriptableObject
{
    
        public SkillKind kindA;
        public SkillKind kindB;

        public SkillKind kindC;
    
}
