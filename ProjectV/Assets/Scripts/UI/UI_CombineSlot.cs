using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UI_CombineSlot : MonoBehaviour
{
    public SkillKind kindA;
    public SkillKind kindB;
    public SkillKind kindC;
    public Image skillA;
    public Image skillB;
    public Image skillC;
    public Image borderA;
    public Image borderB;
    public Image borderC;
    public Image BanIcon;
    private bool isActivateA = false;
    private bool isActivateB = false;
    private Button button;

    private void Start()
    {
        BanIcon.gameObject.SetActive(false);
        BanSkill();
    }
    private void OnEnable()
    {
        BanIcon.gameObject.SetActive(false);
        BanSkill();
    }

    private void BanSkill()
    {
        if (Player.Instance.FindSkill(kindC) != null)
        {
            BanIcon.gameObject.SetActive(true); 
        }
    }
    public void ActivateSkillA(bool isActivate, Color color)
    {
        if (isActivate)
        {
            isActivateA = true;
            if(isActivateA && isActivateB)
            {
                ActivateSlot();
            }
        }
        else
            isActivateA = false;
        skillA.color = color;
    }

    public void ActivateSkillB(bool isActivate, Color color)
    {
        if (isActivate)
        {
            isActivateB = true;
            if(isActivateB && isActivateA)
            {
                ActivateSlot();
            }
        }
        else
            isActivateB = false;
        skillB.color = color;
    }

    private void ActivateSlot()
    {
        if (!button.interactable)
        {
            button.interactable = true;
        }
    }

    public void Init(Color color)
    {
        button = GetComponent<Button>();
        button.interactable = false;
        skillA.color = color;
        skillB.color = color;
    }

    public void OnClickSlot()
    {
        button.interactable = false;
        UI_CombinePanel.instance.OnClickSlot(kindA, kindB);
        CombineSkillManager.Instance.CombineSkill(kindC);
    }

}
