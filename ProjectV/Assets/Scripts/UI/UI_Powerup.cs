using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Powerup : UI
{
    public static UI_Powerup instacne;

    [SerializeField] private UI_Powerup_statDB m_PowerupDB;
    [SerializeField] private GameObject m_PowerupTemplate;
    [SerializeField] private GameObject m_ContentView;
    [SerializeField] private EventSystem m_EventHandle;
    [SerializeField] private GameObject m_UnderPanel;

    private List<GameObject> PowerupTemplateList = new List<GameObject>();
    //TemplateUI Ver,Ho Rect

    [SerializeField] private int ColumnCount;
    [SerializeField] private float RowPadding;
    [SerializeField] private float ColumnPadding;
    [SerializeField] private float RowInterval;
    [SerializeField] private float ColumnInterval;
    [SerializeField] private float RowPivot;
    [SerializeField] private float ColumnPivot;

    [SerializeField] private Text m_UnderPowerupName;
    [SerializeField] private Image m_UnderPowerupImage;
    [SerializeField] private Text m_UnderPowerupExplan;
    [SerializeField] private Text m_UnderMoneyText;
    private Powerup_DataType m_CurrentPowerupDB;

    public UnityEvent OnBuyButton;

  protected override void Awake()
    {
        instacne = this;
    }

    
    protected override void Start()
    {
        base.Start();
        DataManager.Instance.PriceReset();
        InitPowerupTemplate();
        OnBuyButton.AddListener(ResetTemplate);
        Hide();
    }

    

    private void InitPowerupTemplate()
    {
        m_UnderPowerupImage.color = new Color(1f,1f,1f,0f);
        ColumnInterval = (m_PowerupTemplate.GetComponent<RectTransform>().rect.width);
        RowInterval = (m_PowerupTemplate.GetComponent<RectTransform>().rect.height);
        int DBCount = m_PowerupDB.GetCount();
        GameObject tempobject;
        int tempcolumncount;
        int temprowcount;
        for (int repeat = 0; repeat < DBCount; ++repeat)
        {
            if (repeat == 0)
            {
                tempcolumncount = 0;
                temprowcount = 0;
            }
            else
            {
                tempcolumncount = repeat % ColumnCount;
                temprowcount = repeat / ColumnCount;
            }
            tempobject = Instantiate(m_PowerupTemplate);
            PowerupTemplateList.Add(tempobject);
            tempobject.transform.SetParent(m_ContentView.transform);
            tempobject.transform.localScale = m_PowerupTemplate.transform.localScale;
            tempobject.transform.localPosition = new Vector3((tempcolumncount * (ColumnInterval + ColumnPadding)) + ColumnPivot, -(temprowcount * (RowInterval + RowPadding) + RowPivot), 0f);
            UI_PowerupTemplate tempPUT = (UI_PowerupTemplate)(tempobject.transform.GetComponent("UI_PowerupTemplate"));
            tempPUT.Init(m_PowerupDB.Powerup_Type_List[repeat]);
        }
    }

    public void OnClickResetButton()
    {
        DataManager.Instance.PowerupReset();
        ResetTemplate();
        if (m_CurrentPowerupDB != null)
        {
            m_UnderPowerupName.text = m_CurrentPowerupDB.Powerup_Name.Localized();
            m_UnderPowerupImage.sprite = m_CurrentPowerupDB.Powerup_Image;
            m_UnderPowerupExplan.text = m_CurrentPowerupDB.Powerup_Tip.Localized();
            m_UnderMoneyText.text = m_CurrentPowerupDB.CurrentPowerupPrice.ToString();
        }
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    public void ResetTemplate()
    {
        int repeatcount = PowerupTemplateList.Count;
        for(int repeat =0; repeat < repeatcount; ++repeat)
        {
            PowerupTemplateList[repeat].GetComponent<UI_PowerupTemplate>().ResetRankImage();
        }
    }

    public void PowerupExplanInit(Powerup_DataType data)
    {
        m_UnderPowerupImage.color = new Color(1f, 1f, 1f, 1f);
        m_CurrentPowerupDB = data;
        m_UnderPowerupName.text = data.Powerup_Name.Localized();
        m_UnderPowerupImage.sprite = data.Powerup_Image;
        m_UnderPowerupExplan.text = data.Powerup_Tip.Localized();
        m_UnderMoneyText.text = data.CurrentPowerupPrice.ToString();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    public void OnClickBuyButton()
    {
        DataManager.Instance.BuyPowerup(m_CurrentPowerupDB);
        DataManager.Instance.PriceReset();
        OnBuyButton.Invoke();
        m_UnderPowerupName.text = m_CurrentPowerupDB.Powerup_Name.Localized();
        m_UnderPowerupImage.sprite = m_CurrentPowerupDB.Powerup_Image;
        m_UnderPowerupExplan.text = m_CurrentPowerupDB.Powerup_Tip.Localized();
        m_UnderMoneyText.text = m_CurrentPowerupDB.CurrentPowerupPrice.ToString();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
}
