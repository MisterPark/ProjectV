using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
{

    private RectTransform rectTransform;
    [SerializeField] private RectTransform background;
    public UI_SlotInfomation[] slot;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetSkillInfomations(List<Skill> skills)
    {
        int i = 0;
        foreach (Skill skill in skills)
        {
            //SkillData data = DataManager.Instance.skillDatas[(int)item.kind].skillData;
            //slot.icon.sprite = data.icon;
            //slot.text.text = "LV." + (item.nextLevel).ToString();
            SkillData data = skill.SkillData;
            slot[i].icon.sprite = data.icon;
            if (skill.level != skill.maxLevel)
                slot[i].text.text = "LV. " + skill.level.ToString();
            else
                slot[i].text.text = "LV. " +
                    "MAX";
            i++;
        }
    }
}
