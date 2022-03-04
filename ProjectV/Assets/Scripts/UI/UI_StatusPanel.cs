using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatusPanel : UI
{
    public static UI_StatusPanel instance;

    [SerializeField] private RectTransform equipment;
    private UI_Equipment equipmentUI;
    void Start()
    {
        instance = this;

        equipmentUI = equipment.GetComponent<UI_Equipment>();
    }

    public void SetSkillInfomations(List<Skill> skills)
    {
        equipmentUI.SetSkillInfomations(skills);
    }

}
