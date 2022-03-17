using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineSkillManager : MonoBehaviour
{
    public static CombineSkillManager Instance;
    
    public CombineSkillDataElement[] combineSkillDatas;

    public void CombineSkill(SkillKind skillKind)
    {
        foreach(var skill in combineSkillDatas)
        {
            if(skill.combinedSkill != skillKind)
            {
                Debug.Log(skillKind+"해당 스킬의 조합이 없습니다.");
                return;
            }

            Player.Instance.RemoveSkill(skill.combineSkillData.materialA);
            Player.Instance.RemoveSkill(skill.combineSkillData.materialB);

            Player.Instance.AddOrIncreaseSkill(skill.combineSkillData.combinedSkill);
        }
    
    }
}
