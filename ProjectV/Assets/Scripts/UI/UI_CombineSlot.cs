using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void ActivateSkillA(bool isActivate, Color color)
    {
        if (isActivate)
            isActivateA = true;
        else
            isActivateA = false;
        skillA.color = color;
    }

    public void ActivateSkillB(bool isActivate, Color color)
    {
        if (isActivate)
            isActivateB = true;
        else
            isActivateB = false;
        skillB.color = color;
    }

    public bool ActivateSlot()
    {
        if (isActivateA == true && isActivateB == true)
        {
            button.interactable = true;
            return true;
        }
        return false;
    }

    public void Init(Color color)
    {
        skillA.color = color;
        skillB.color = color;
    }

    public void OnClickSlot()
    {
        button.interactable = false;
        CombineSkillManager.Instance.CombineSkill(kindC);
    }

}
