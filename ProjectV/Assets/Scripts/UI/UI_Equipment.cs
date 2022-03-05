using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
{

    private RectTransform rectTransform;
    [SerializeField] private RectTransform background;
    public UI_SlotInfomation[] activeSlot;
    public UI_SlotInfomation[] passiveSlot;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        HideAllSlot();
    }

    public void SetSkillInfomations(List<Skill> skills)
    {
        int i = 0;
        int j = 0;
        foreach (Skill skill in skills)
        {
            SkillData data = skill.SkillData;
            if(data.type == SkillType.Active)
            {
                activeSlot[i].icon.sprite = data.icon;
                if (skill.level != skill.maxLevel)
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
                if (skill.level != skill.maxLevel)
                    passiveSlot[j].text.text = "LV. " + skill.level.ToString();
                else
                    passiveSlot[j].text.text = "LV. " +
                        "MAX";
                passiveSlot[j].Show();
                j++;
            }
        }
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
}
