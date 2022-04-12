using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviourEx
{
    public bool isTextHide = false;
    private RectTransform rectTransform;
    [SerializeField] private RectTransform background;
    public UI_SlotInfomation[] activeSlot;
    public UI_SlotInfomation[] passiveSlot;

    
    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        Player.Instance.OnAddOrIncreaseSkill.AddListener(OnSkillSeletionCompleteCallback);
        HideAllSlot();
        SetSkillInfomations(Player.Instance.Skills);
        if (isTextHide)
        {
            ResetSize();
            HideText();
        }
    }

    public void SetSkillInfomations(List<Skill> skills)
    {
        int i = 0;
        int j = 0;

        for (int k = 0; k < 6; k++)
        {
            activeSlot[k].Hide();
            passiveSlot[k].Hide();
        }

        for (int k = 0; k < skills.Count; k++)
        {
            Skill skill = skills[k];
            if (skill == null)
                continue;
            SkillData data = DataManager.Instance.skillDatas[(int)skill.Kind].skillData;
            if (skill.Kind == SkillKind.None)
                continue;
            if (data.type == SkillType.Active)
            {
                activeSlot[i].icon.sprite = data.icon;
                if (skill.level != skill.MaxLevel)
                    activeSlot[i].text.text = "LV. " + skill.level.ToString();
                else
                    activeSlot[i].text.text = "LV. " +
                        "MAX";
                activeSlot[i].Show();
                i++;
            }
            else if(data.type == SkillType.Passive)
            {
                passiveSlot[j].icon.sprite = data.icon;
                if (skill.level != skill.MaxLevel)
                    passiveSlot[j].text.text = "LV. " + skill.level.ToString();
                else
                    passiveSlot[j].text.text = "LV. " +
                        "MAX";
                passiveSlot[j].Show();
                j++;
            }
        }
    }

    void OnSkillSeletionCompleteCallback()
    {
        SetSkillInfomations(Player.Instance.Skills);
    }

    public void HideAllSlot()
    {
        int activeCnt = activeSlot.Length;
        int passiveCnt = passiveSlot.Length;
        for(int i =0; i < activeCnt; i++)
        {
            activeSlot[i].Hide();
        }
        for(int i = 0; i < passiveCnt; i++)
        {
            passiveSlot[i].Hide();
        }
    }

    private void ResetSize()
    {
        Vector2 vecSize = transform.parent.GetComponent<RectTransform>().sizeDelta;
        int size = Mathf.RoundToInt((vecSize.x * 0.034f));
        int width = size * 6;
        int height = size * 2;
        float posY = vecSize.y * 0.05f;
        rectTransform.sizeDelta = new Vector2(width, height);
        rectTransform.anchoredPosition = new Vector2(0f, -posY - 1f);
    }

    private void HideText()
    {
        RectTransform image;
        int activeCnt = activeSlot.Length;
        int passiveCnt = passiveSlot.Length;
        for (int i = 0; i < activeCnt; i++)
        {
            activeSlot[i].text.gameObject.SetActive(false);
            image = activeSlot[i].icon.gameObject.GetComponent<RectTransform>();
            image.anchorMin = new Vector2(0f, 0f);
            image.anchorMax = new Vector2(1f, 1f);
            image.pivot = new Vector2(0.5f, 0.5f);
            image.sizeDelta = Vector2.zero;
            image.anchoredPosition = Vector2.zero;
        }
        for (int i = 0; i < passiveCnt; i++)
        {
            passiveSlot[i].text.gameObject.SetActive(false);
            image = passiveSlot[i].icon.gameObject.GetComponent<RectTransform>();
            image.anchorMin = new Vector2(0f, 0f);
            image.anchorMax = new Vector2(1f, 1f);
            image.pivot = new Vector2(0.5f, 0.5f);
            image.sizeDelta = Vector2.zero;
            image.anchoredPosition = Vector2.zero;
        }
        background.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }
}
