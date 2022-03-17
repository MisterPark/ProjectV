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
    [SerializeField]
    private RectTransform status;
    private UI_Equipment equipmentUI;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        equipmentUI = equipment.GetComponent<UI_Equipment>();
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();
        ResetSize();
        Hide();
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
        float statusPosY = equipmentPosY - equipment.sizeDelta.y;
        float panelScaleY = height / 1080f;
        rectTransform.sizeDelta = new Vector2(width, height);
        rectTransform.localScale = new Vector3(1f, panelScaleY, 0f);
        equipment.anchoredPosition = new Vector2(0f, equipmentPosY);
        status.anchoredPosition = new Vector2(0f, statusPosY);
    }
}
