using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CombinePanel : UI
{
    public GameObject contentsObject;
    public GameObject contentPrefab;
    private Dictionary<SkillKind, int> playerSkillLevel = new Dictionary<SkillKind, int>();
    private Color disableColor = new Color(0.3f, 0.3f, 0.3f, 0.9f);
    private Color ableColor = new Color(1f, 1f, 1f, 1f);
    private UI_CombineSlot[] contents;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        Player.Instance.OnAddOrIncreaseSkill.AddListener(OnSkillSeletionCompleteCallback);
        Hide();
    }

    private void SetPlayerSkillInformation(List<Skill> playerSkill)
    {
        for (int i = 0; i < playerSkill.Count; i++)
        {
            Skill skill = playerSkill[i];
            SkillData data = DataManager.Instance.skillDatas[(int)skill.Kind].skillData;
            if (data.type == SkillType.Active)
            {
                if (playerSkillLevel.ContainsKey(skill.Kind))
                {
                    playerSkillLevel[skill.Kind] = skill.level;
                }
                else
                {
                    playerSkillLevel.Add(skill.Kind, skill.level);
                }
            }
        }
    }

    private void CheckSkillCombinable()
    {
        for(int i = 0; i< contents.Length; i++)
        {
            if (playerSkillLevel.ContainsKey(contents[i].kindA))
            {
                if (playerSkillLevel[contents[i].kindA] == DataManager.Instance.skillDatas[(int)contents[i].kindA].skillData.maxLevel)
                {
                    contents[i].ActivateSkillA(true, ableColor);
                }
                else
                    contents[i].ActivateSkillA(false, disableColor);
            }
            else
                contents[i].ActivateSkillA(false, disableColor);

            if (playerSkillLevel.ContainsKey(contents[i].kindB))
            {
                if (playerSkillLevel[contents[i].kindB] == DataManager.Instance.skillDatas[(int)contents[i].kindB].skillData.maxLevel)
                {
                    contents[i].ActivateSkillB(true, ableColor);
                }
                else
                    contents[i].ActivateSkillB(false, disableColor);
            }
            else
                contents[i].ActivateSkillB(false, disableColor);

            contents[i].ActivateSlot();
        }
    }

    private void OnSkillSeletionCompleteCallback()
    {
        SetPlayerSkillInformation(Player.Instance.Skills);
        CheckSkillCombinable();
    }

    private void Init()
    {
        contents = new UI_CombineSlot[CombineSkillManager.Instance.combineSkillDatas.Length];
        for(int i =0; i < contents.Length; i++)
        {
            GameObject TempObject = Instantiate(contentPrefab);
            contents[i] = TempObject.GetComponent<UI_CombineSlot>();
            TempObject.transform.parent = contentsObject.transform;
            TempObject.transform.localScale = Vector3.one;

            CombineSkill combineData = CombineSkillManager.Instance.combineSkillDatas[i].combineSkillData;
            contents[i].kindA = combineData.materialA;
            contents[i].kindB = combineData.materialB;
            contents[i].kindC = combineData.combinedSkill;

            contents[i].skillA.sprite = DataManager.Instance.skillDatas[((int)(contents[i].kindA))].skillData.icon;
            contents[i].skillB.sprite = DataManager.Instance.skillDatas[((int)(contents[i].kindB))].skillData.icon;
            contents[i].skillC.sprite = DataManager.Instance.skillDatas[((int)(contents[i].kindC))].skillData.icon;

            contents[i].Init(disableColor);
        }

    }
}
