using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CharacterSelect : MonoBehaviour
{
    [SerializeField] private EventSystem Event_Handle;
    [SerializeField] private TMPro.TextMeshProUGUI CharacterName;
    [SerializeField] private Image WeaponImage;
    [SerializeField] private Image CharacterImage;
    [SerializeField] private TMPro.TextMeshProUGUI DescriptionText;
    [SerializeField] private GameObject CharacterSlot;
    [SerializeField] private GameObject ContentsWindow;

    [SerializeField] private float SlotXPosition;
    [SerializeField] private float SlotYPosition;
    [SerializeField] private float SlotXPadding;
    [SerializeField] private float SlotYPadding;
    [SerializeField] private int SlotColumnCount;
    private float SlotWidth;
    private float SlotHeight;

    private PlayerCharacterName CurrentClickPlayerCharacter;
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

    void DescriptionPanelInit(string charname, Sprite charimage, Sprite weaponimage, string descrip)
    {
        CharacterName.text = charname;
        CharacterImage.sprite = charimage;
        WeaponImage.sprite = weaponimage;
        DescriptionText.text = descrip;
    }

    public void OnClickCharacterBoard()
    {
        Event_Handle = EventSystem.current;
        GameObject selectobject = Event_Handle.currentSelectedGameObject;
        CharacterName.text = selectobject.GetComponent<UI_CharacterSlot>().CharacterName.text;
        CharacterImage.sprite = selectobject.GetComponent<UI_CharacterSlot>().CharacterImage.sprite;
        WeaponImage.sprite = selectobject.GetComponent<UI_CharacterSlot>().WeaponImage.sprite;
        DescriptionText.text = selectobject.GetComponent<UI_CharacterSlot>().m_CharacterDescription;
        CurrentClickPlayerCharacter = selectobject.GetComponent<UI_CharacterSlot>().CharacterIndex;
    }

    void SlotInit()
    {
        GameObject tempslot;
        float tempwidth;
        float tempheight;
        int column;
        int row;

        SlotWidth = CharacterSlot.GetComponent<RectTransform>().rect.width;
        SlotHeight = CharacterSlot.GetComponent<RectTransform>().rect.height;
        for (int repeat = 0; repeat < (int)PlayerCharacterName.END; ++repeat)
        {
            column = repeat % SlotColumnCount;
            row = repeat / SlotColumnCount;
            tempslot = Instantiate(CharacterSlot);
            tempslot.transform.SetParent(ContentsWindow.transform);
            tempwidth = SlotWidth * tempslot.transform.localScale.x;
            tempheight = SlotHeight * tempslot.transform.localScale.y;
            tempslot.transform.localPosition = new Vector3(SlotXPosition+((tempwidth + SlotXPadding) * column), SlotYPosition - ((tempheight + SlotYPadding) * row), 0f);

            tempslot.GetComponent<UI_CharacterSlot>().CharacterImage.sprite = DataManager.Instance.playerCharacterData[repeat].charImage;
            tempslot.GetComponent<UI_CharacterSlot>().CharacterName.text = DataManager.Instance.playerCharacterData[repeat].name.ToString();
            tempslot.GetComponent<UI_CharacterSlot>().WeaponImage.sprite = DataManager.Instance.skillDatas[(int)DataManager.Instance.playerCharacterData[repeat].firstSkill].skillData.icon;
            tempslot.GetComponent<UI_CharacterSlot>().m_CharacterDescription = DataManager.Instance.playerCharacterData[repeat].description;
            tempslot.GetComponent<UI_CharacterSlot>().CharacterIndex = (PlayerCharacterName)repeat;
        }
    }

    public void OnClickCharacterSelectOKButton()
    {
        DataManager.Instance.currentPlayerCharacter = CurrentClickPlayerCharacter;
        DataManager.Instance.Setting_PowerStat();
        SceneManager.LoadScene(UIManager.Instance.StartSceneName);
    }
}
