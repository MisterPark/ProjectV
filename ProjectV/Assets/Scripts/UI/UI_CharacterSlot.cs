using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSlot : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterSelect = this.transform.root.Find("CharacterSelectPanel").gameObject;
        Button tempbutton = GetComponent<Button>();
        tempbutton.onClick.AddListener(m_CharacterSelect.GetComponent<UI_CharacterSelect>().OnClickCharacterBoard);
        tempbutton.onClick.AddListener(UI_CharacterSelectStatus.Instance.Setting_UIStatus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
