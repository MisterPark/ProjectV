using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StageSelect : UI
{
    public static UI_StageSelect instance;
    [SerializeField] private EventSystem Event_Handle;
    [SerializeField] private GameObject StageSelectSlot;
    [SerializeField] private GameObject ContentsWindow;
    [SerializeField] private Text DescriptionName;
    [SerializeField] private Image DescriptionImage;
    [SerializeField] private Text DescriptionText;
    private string stageName;
    private StageKind CurrentStageKind;

    [SerializeField] private float SlotYPosition;
    [SerializeField] private float SlotYPadding;

    private float SlotHeight;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //StageSelectSlot = this.transform.root.Find("CharacterSlot").gameObject;
        SlotInit();
        Hide();
        //gameObject.SetActive(false);
        
    }

    public void OnClickStageSlot()
    {
        DescriptionImage.color = new Color(1f, 1f, 1f, 1f);
        Event_Handle = EventSystem.current;
        GameObject selectobject = Event_Handle.currentSelectedGameObject;
        DescriptionName.text = selectobject.GetComponent<UI_StageSlot>().SlotName.text;
        DescriptionText.text = selectobject.GetComponent<UI_StageSlot>().SlotDescription.text;
        DescriptionImage.sprite = selectobject.GetComponent<UI_StageSlot>().SlotImage.sprite;
        CurrentStageKind = selectobject.GetComponent<UI_StageSlot>().Stage;
        for(int i =0; i < DataManager.Instance.stageDatas.Length; i++)
        {
            if(CurrentStageKind == DataManager.Instance.stageDatas[i].kind)
            {
                stageName = DataManager.Instance.stageDatas[i].stageName;
                break;
            }
        }
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    void SlotInit()
    {
        DescriptionImage.color = new Color(1f,1f,1f,0f);
        GameObject tempslot;
        for (int repeat = 0; repeat < (int)(StageKind.End); ++repeat)
        {
            tempslot = Instantiate(StageSelectSlot);
            tempslot.transform.SetParent(ContentsWindow.transform);
            SlotHeight = tempslot.GetComponent<RectTransform>().rect.height;
            tempslot.transform.localScale = StageSelectSlot.transform.localScale;
            tempslot.transform.localPosition = new Vector3(0f, SlotYPosition - ((SlotHeight + SlotYPadding) * repeat), 0f);

            tempslot.GetComponent<UI_StageSlot>().SlotName.text = DataManager.Instance.stageDatas[repeat].stageName;
            tempslot.GetComponent<UI_StageSlot>().SlotImage.sprite = DataManager.Instance.stageDatas[repeat].icon;
            tempslot.GetComponent<UI_StageSlot>().SlotDescription.text = DataManager.Instance.stageDatas[repeat].description;
            tempslot.GetComponent<UI_StageSlot>().Stage = (StageKind)repeat;
        }
       
    }

    public void OnClickStageSelectOKButton()
    {
        if (stageName!=null)
        {
            LoadingSceneManager.instance.LoadScene(stageName);
        }
        else
        {
            Debug.Log("스테이지를 선택해주세요");
        }
    }
}
