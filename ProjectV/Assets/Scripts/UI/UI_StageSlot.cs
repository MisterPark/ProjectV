using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageSlot : MonoBehaviourEx
{
    public Text SlotName;
    public Image SlotImage;
    public Text SlotDescription;
    public StageKind Stage;
    private GameObject StageSelect;
    
    protected override void Start()
    {
        base.Start();
        StageSelect = this.transform.root.Find("StageSelectPanel").gameObject;
        Button tempbutton = GetComponent<Button>();
        tempbutton.onClick.AddListener(StageSelect.GetComponent<UI_StageSelect>().OnClickStageSlot);
    }
}
