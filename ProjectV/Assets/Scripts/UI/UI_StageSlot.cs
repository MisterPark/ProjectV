using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageSlot : MonoBehaviour
{
    public Text SlotName;
    public Image SlotImage;
    public Text SlotDescription;
    public StageKind Stage;
    private GameObject StageSelect;
    
    void Start()
    {
        StageSelect = this.transform.root.Find("StageSelectPanel").gameObject;
        Button tempbutton = GetComponent<Button>();
        tempbutton.onClick.AddListener(StageSelect.GetComponent<UI_StageSelect>().OnClickStageSlot);
    }
}
