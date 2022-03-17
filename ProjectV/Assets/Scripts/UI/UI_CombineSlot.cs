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

    private bool isActivateA = false;
    private bool isActivateB = false;
    private Button button;
    private UI_CombinePanel parent;
    // Start is called before the first frame update
    void Start()
    {
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

    public void Init(UI_CombinePanel parentPanel, Color color)
    {
        button = GetComponent<Button>();
        button.interactable = false;
        parent = parentPanel;
        skillA.color = color;
        skillB.color = color;
    }

    public void OnClickSlot()
    {
        button.interactable = false;
        parent.OnClickSlot(kindA, kindB);
        CombineSkillManager.Instance.CombineSkill(kindC);
    }

}
