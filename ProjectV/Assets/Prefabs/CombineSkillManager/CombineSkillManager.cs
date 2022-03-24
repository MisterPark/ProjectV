using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineSkillManager : MonoBehaviour
{
    public static CombineSkillManager Instance;
    
    public CombineSkillDataElement[] combineSkillDatas;

    private void Start()
    {
        Instance = this;
        //for (int i = 0; i < combineSkillDatas.Length; i++)
        //{
        //    Debug.Log(combineSkillDatas[i].combinedSkill);
        //}
    }

    public void CombineSkill(SkillKind skillKind)
    {
        foreach(var skill in combineSkillDatas)
        {
            if(skill.combinedSkill != skillKind)
            {
                continue;
            }

            Player.Instance.RemoveSkill(skill.combineSkillData.materialA);
            Debug.Log(skill.combineSkillData.materialA + "제거");
            Player.Instance.RemoveSkill(skill.combineSkillData.materialB);
            Debug.Log(skill.combineSkillData.materialB + "제거");
            Player.Instance.AddOrIncreaseSkill(skill.combineSkillData.combinedSkill);
            Debug.Log(skill.combineSkillData.combinedSkill + "추가");
        }
    
    }
}
