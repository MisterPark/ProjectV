using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageSlot : MonoBehaviour
{
    public TMPro.TextMeshProUGUI SlotName;
    public Image SlotImage;
    public TMPro.TextMeshProUGUI SlotDescription;
    public StageKind Stage;
    private GameObject StageSelect;
    // Start is called before the first frame update
    void Start()
    {
        StageSelect = this.transform.root.Find("StageSelectPanel").gameObject;
        Button tempbutton = GetComponent<Button>();
        tempbutton.onClick.AddListener(StageSelect.GetComponent<UI_StageSelect>().OnClickStageSlot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}