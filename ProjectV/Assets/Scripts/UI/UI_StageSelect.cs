using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StageSelect : MonoBehaviour
{
    [SerializeField] private EventSystem Event_Handle;
    [SerializeField] private GameObject StageSelectSlot;
    [SerializeField] private GameObject ContentsWindow;
    [SerializeField] private TMPro.TextMeshProUGUI DescriptionName;
    [SerializeField] private Image DescriptionImage;
    [SerializeField] private TMPro.TextMeshProUGUI DescriptionText;
    private StageKind CurrentStageKind;

    [SerializeField] private float SlotYPosition;
    [SerializeField] private float SlotYPadding;

    private float SlotHeight;
    // Start is called before the first frame update
    void Start()
    {
        SlotInit();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStageSlot()
    {
        Event_Handle = EventSystem.current;
        GameObject selectobject = Event_Handle.currentSelectedGameObject;
        DescriptionName.text = selectobject.GetComponent<UI_StageSlot>().SlotName.text;
        DescriptionText.text = selectobject.GetComponent<UI_StageSlot>().SlotDescription.text;
        DescriptionImage.sprite = selectobject.GetComponent<UI_StageSlot>().SlotImage.sprite;
        CurrentStageKind = selectobject.GetComponent<UI_StageSlot>().Stage;
        UIManager.Instance.StartSceneName = CurrentStageKind.ToString();
    }

    void SlotInit()
    {
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
        SceneManager.LoadScene(UIManager.Instance.StartSceneName);
    }
}
