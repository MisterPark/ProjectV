using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSlot : MonoBehaviourEx
{
    [SerializeField] private Image m_CharacterImage;
    [SerializeField] private Image m_WeaponImage;
    [SerializeField] private Text m_CharacterName;
    private GameObject m_CharacterSelect;
    public string m_CharacterDescription;
    public PlayerCharacterName CharacterIndex;

    public Image CharacterImage => m_CharacterImage;
    public Image WeaponImage => m_WeaponImage;
    public Text CharacterName => m_CharacterName;

    
    protected override void Start()
    {
        base.Start();
        m_CharacterSelect = this.transform.root.Find("CharacterSelectPanel").gameObject;
        Button tempbutton = GetComponent<Button>();
        tempbutton.onClick.AddListener(m_CharacterSelect.GetComponent<UI_CharacterSelect>().OnClickCharacterBoard);
        tempbutton.onClick.AddListener(UI_CharacterSelectStatus.Instance.Setting_UIStatus);
    }

    
    public override void UpdateEx()
    {
        
    }
}
