using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Powerup : MonoBehaviour
{
    [SerializeField] private UI_Powerup_statDB m_PowerupDB;
    [SerializeField] private GameObject m_PowerupTemplate;
    [SerializeField] private GameObject m_ContentView;
    [SerializeField] private EventSystem m_EventHandle;
    [SerializeField] private GameObject m_UnderPanel;
    [SerializeField] private Image m_aaa;
    [SerializeField] private TMPro.TextMeshProUGUI m_bbb;
    [SerializeField] private TMPro.TextMeshProUGUI m_ccc;

    //TemplateUI Ver,Ho Rect

    [SerializeField] private int ColumnCount;
    [SerializeField] private float RowPadding;
    [SerializeField] private float ColumnPadding;
    [SerializeField] private float RowInterval;
    [SerializeField] private float ColumnInterval;
    [SerializeField] private float RowPivot;
    [SerializeField] private float ColumnPivot;

    // Start is called before the first frame update
    void Start()
    {
        InitPowerupTemplate();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void InitPowerupTemplate()
    {
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
            tempobject.transform.SetParent(m_ContentView.transform);
            tempobject.transform.localScale = m_PowerupTemplate.transform.localScale;
            tempobject.transform.localPosition = new Vector3((tempcolumncount * (ColumnInterval + ColumnPadding)) + ColumnPivot, -(temprowcount * (RowInterval + RowPadding) + RowPivot), 0f);
            UI_PowerupTemplate tempPUT = (UI_PowerupTemplate)(tempobject.transform.GetComponent("UI_PowerupTemplate"));
            tempPUT.Init(m_PowerupDB.Powerup_Type_List[repeat]);
        }
    }

    public void Powerup_OnCursor()
    {
        m_EventHandle = EventSystem.current;
        if (m_EventHandle != null)
        {
            GameObject tep = m_EventHandle.currentSelectedGameObject;
            if (tep != null)
            {
                GameObject tempobject = m_EventHandle.currentSelectedGameObject;

                m_UnderPanel.GetComponent<UI_PowerupUnderPanel>().PowerupExplanInit(tempobject.GetComponent<UI_PowerupTemplate>().DataType);
                m_aaa.sprite = tempobject.GetComponent<UI_PowerupTemplate>().DataType.Powerup_Image;
                m_bbb.text = tempobject.GetComponent<UI_PowerupTemplate>().DataType.Powerup_Name;
                m_ccc.text = tempobject.GetComponent<UI_PowerupTemplate>().DataType.Powerup_Tip;
                LayoutRebuilder.ForceRebuildLayoutImmediate(m_UnderPanel.GetComponent<RectTransform>());
            }
        }
    }
}
