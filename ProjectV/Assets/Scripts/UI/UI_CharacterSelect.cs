using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_CharacterSelect : UI
{
    public static UI_CharacterSelect instance;
    [SerializeField] private EventSystem Event_Handle;
    [SerializeField] private Text CharacterName;
    [SerializeField] private Image WeaponImage;
    [SerializeField] private Image CharacterImage;
    [SerializeField] private Text DescriptionText;
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

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        SlotInit();
        gameObject.SetActive(false);
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
        
        DataManager.Instance.currentGameData.characterName = CurrentClickPlayerCharacter;
        DataManager.Instance.currentGameData.gold = 0;
        DataManager.Instance.currentGameData.killCount = 0;
        DataManager.Instance.currentGameData.totalPlayTime = 0;
        DataManager.Instance.currentGameData.playerLevel = 1;

        DataManager.Instance.Setting_PowerStat();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    void SlotInit()
    {
        GameObject tempslot;
        SlotWidth = CharacterSlot.GetComponent<RectTransform>().rect.width;
        SlotHeight = CharacterSlot.GetComponent<RectTransform>().rect.height;
        for (int repeat = 0; repeat <= (int)PlayerCharacterName.King; ++repeat)
        {
            //column = repeat % SlotColumnCount;
            //row = repeat / SlotColumnCount;
            PlayerCharacterData character = DataManager.Instance.playerCharacterData[repeat];

            tempslot = Instantiate(CharacterSlot);
            tempslot.transform.SetParent(ContentsWindow.transform);
            tempslot.transform.localScale = Vector3.one;
            
            tempslot.GetComponent<UI_CharacterSlot>().CharacterImage.sprite = character.playerCharacter.charImage;
            tempslot.GetComponent<UI_CharacterSlot>().CharacterName.text = character.playerCharacter.characterName.ToString();
            tempslot.GetComponent<UI_CharacterSlot>().WeaponImage.sprite = DataManager.Instance.skillDatas[(int)character.playerCharacter.firstSkill].skillData.icon;
            tempslot.GetComponent<UI_CharacterSlot>().m_CharacterDescription = character.playerCharacter.description;
            tempslot.GetComponent<UI_CharacterSlot>().CharacterIndex = (PlayerCharacterName)repeat;
        }
        {
            DataManager.Instance.currentGameData.characterName = PlayerCharacterName.Witch;
            GameObject selectobject = transform.Find("Scroll View").Find("Contents").GetChild(0).gameObject;
            CharacterName.text = selectobject.GetComponent<UI_CharacterSlot>().CharacterName.text;
            CharacterImage.sprite = selectobject.GetComponent<UI_CharacterSlot>().CharacterImage.sprite;
            WeaponImage.sprite = selectobject.GetComponent<UI_CharacterSlot>().WeaponImage.sprite;
            DescriptionText.text = selectobject.GetComponent<UI_CharacterSlot>().m_CharacterDescription;
            CurrentClickPlayerCharacter = selectobject.GetComponent<UI_CharacterSlot>().CharacterIndex;
            DataManager.Instance.Setting_PowerStat();
        }
    }

    public void OnClickCharacterSelectOKButton()
    {
        DataManager.Instance.currentGameData.characterName = CurrentClickPlayerCharacter;
        gameObject.SetActive(false);
        UI_StageSelect.instance.Show();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
}
