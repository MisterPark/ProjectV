using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CombinePanel : UI
{
    public static UI_CombinePanel instance;

    public GameObject contentsObject;
    public GameObject contentPrefab;

    private bool isInit = false;
    private Dictionary<SkillKind, int> playerSkillLevel = new Dictionary<SkillKind, int>();
    private Color disableColor = new Color(0.3f, 0.3f, 0.3f, 0.9f);
    private Color ableColor = new Color(1f, 1f, 1f, 1f);
    private UI_CombineSlot[] contents;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isInit = Init();
        if (Player.Instance == null || isInit == false)
        {
            Hide();
            return;
        }
        SetPlayerSkillInformation(Player.Instance.Skills);
        CheckSkillCombinable();
        Hide();
    }

    private void OnEnable()
    {
        if (Player.Instance == null)
            return;
        if(!isInit)
        {
            isInit = Init();
            if(isInit == false)
            {
                Hide();
                return;
            }
        }
        SetPlayerSkillInformation(Player.Instance.Skills);
        CheckSkillCombinable();
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
        //for(int i = 0; i< contents.Length; i++)
        //{
        //    if (playerSkillLevel.ContainsKey(contents[i].kindA))
        //    {
        //        if (playerSkillLevel[contents[i].kindA] == DataManager.Instance.skillDatas[(int)contents[i].kindA].skillData.maxLevel)
        //        {
        //            contents[i].ActivateSkillA(true, ableColor);
        //        }
        //        else
        //            contents[i].ActivateSkillA(false, disableColor);
        //    }
        //    else
        //        contents[i].ActivateSkillA(false, disableColor);

        //    if (playerSkillLevel.ContainsKey(contents[i].kindB))
        //    {
        //        if (playerSkillLevel[contents[i].kindB] == DataManager.Instance.skillDatas[(int)contents[i].kindB].skillData.maxLevel)
        //        {
        //            contents[i].ActivateSkillB(true, ableColor);
        //        }
        //        else
        //            contents[i].ActivateSkillB(false, disableColor);
        //    }
        //    else
        //        contents[i].ActivateSkillB(false, disableColor);
        //}
        foreach(KeyValuePair<SkillKind, int> item in playerSkillLevel)
        {
            if(item.Value == DataManager.Instance.skillDatas[((int)(item.Key))].skillData.maxLevel)
            {
                AbleSlot(item.Key);
            }
        }
    }

    private bool Init()
    {
        if (CombineSkillManager.Instance == null)
            return false;
        contents = new UI_CombineSlot[CombineSkillManager.Instance.combineSkillDatas.Length];
        for(int i =0; i < contents.Length; i++)
        {
            GameObject TempObject = Instantiate(contentPrefab);
            contents[i] = TempObject.GetComponent<UI_CombineSlot>();
            TempObject.transform.SetParent(contentsObject.transform);
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
        return true;
    }

    public void OnClickSlot(SkillKind matA, SkillKind matB)
    {
        playerSkillLevel.Remove(matA);
        playerSkillLevel.Remove(matB);
        DisableSlot(matA);
        DisableSlot(matB);
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    private void AbleSlot(SkillKind kind)
    {
        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i].kindA == kind)
            {
                contents[i].ActivateSkillA(true, ableColor);
            }
            if (contents[i].kindB == kind)
            {
                contents[i].ActivateSkillB(true, ableColor);
            }
        }
    }

    private void DisableSlot(SkillKind kind)
    {
        for(int i = 0; i < contents.Length; i++)
        {
            if(contents[i].kindA == kind)
            {
                contents[i].ActivateSkillA(false, disableColor);
            }
            if(contents[i].kindB == kind)
            {
                contents[i].ActivateSkillB(false, disableColor);
            }
        }
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlaySFXSound("ShortButton");
        Hide();
    }


}
