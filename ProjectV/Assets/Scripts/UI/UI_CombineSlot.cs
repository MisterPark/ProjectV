using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UI_CombineSlot : MonoBehaviourEx
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

    protected override void Start()
    {
        base.Start();
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
        }
        else
        {
            isActivateA = false;
        }

        if (isActivateA && isActivateB)
        {
            ActivateSlot();
        }
        else
        {
            DeactivateSlot();
        }
        skillA.color = color;
    }

    public void ActivateSkillB(bool isActivate, Color color)
    {
        if (isActivate)
        {
            isActivateB = true;
        }
        else
        {
            isActivateB = false;
        }

        if (isActivateB && isActivateA)
        {
            ActivateSlot();
        }
        else
        {
            DeactivateSlot();
        }
        skillB.color = color;
    }

    private void ActivateSlot()
    {
        if (!button.interactable)
        {
            Debug.Log(kindC.ToString() + "스킬 활성화");
            button.interactable = true;
        }
    }

    private void DeactivateSlot()
    {
        Debug.Log(kindC.ToString() + "스킬 비활성화");
        button.interactable = false;
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
        CombineSkillManager.Instance.CombineSkill(kindC);
        UI_CombinePanel.instance.OnClickSlot(kindA, kindB);
    }

}
