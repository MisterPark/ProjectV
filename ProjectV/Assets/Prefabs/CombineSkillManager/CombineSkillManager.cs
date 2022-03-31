using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineSkillManager : MonoBehaviourEx
{
    public static CombineSkillManager Instance;
    
    public CombineSkillDataElement[] combineSkillDatas;

    protected override void Start()
    {
        base.Start();
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
            ;

            if (Player.Instance.FindSkill(skillKind) == null)
            {
                Player.Instance.RemoveSkill(skill.combineSkillData.materialA);
                Debug.Log(skill.combineSkillData.materialA + "����");
                Player.Instance.RemoveSkill(skill.combineSkillData.materialB);
                Debug.Log(skill.combineSkillData.materialB + "����");
                Player.Instance.AddOrIncreaseSkill(skill.combineSkillData.combinedSkill);
                Debug.Log(skill.combineSkillData.combinedSkill + "�߰�");
            }
            else
            {
                Debug.Log(skillKind + " �� �̹� �����ϴ� ��ų�Դϴ�.");
            }

        }
    
    }
}
