using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatusPanel : UI
{
    public static UI_StatusPanel instance;

    private RectTransform rectTransform;
    private RectTransform parent;
    [SerializeField]
    private RectTransform equipment;
    private UI_Equipment equipmentUI;

    
    void Start()
    {
        instance = this;

        equipmentUI = equipment.GetComponent<UI_Equipment>();
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();
        ResetSize();
    }

    public void SetSkillInfomations(List<Skill> skills)
    {
        equipmentUI.SetSkillInfomations(skills);
    }

    public void ResetSize()
    {
        float width = parent.sizeDelta.x;
        float height = parent.sizeDelta.y;
        float equipmentPosY = height * -0.07f;
        rectTransform.sizeDelta = new Vector2(width, height);
        equipment.anchoredPosition = new Vector2(0f, equipmentPosY);
    }
}
