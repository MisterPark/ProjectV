using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PowerupUnderPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_PowerupName;
    [SerializeField] private Image m_PowerupImage;
    [SerializeField] private TMPro.TextMeshProUGUI m_PowerupExplan;
    [SerializeField] private TMPro.TextMeshProUGUI m_MoneyText;
    private Powerup_DataType m_CurrentPowerupDB;

    private string Name;
    private string Tip;
    // Start is called before the first frame update
    void Start()
    {
        m_MoneyText.text = DataManager.Instance.currentSaveData.currentGold.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PowerupExplanInit(Powerup_DataType data)
    {
        m_CurrentPowerupDB = data;
        m_PowerupName.text = data.Powerup_Name;
        m_PowerupImage.sprite = data.Powerup_Image;
        m_PowerupExplan.text = data.Powerup_Tip;
    }

    public void OnClickBuyButton()
    {
        int itemp = m_CurrentPowerupDB.Rank + 1;
        m_CurrentPowerupDB.SetRank(itemp);
    }
}
