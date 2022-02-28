using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PowerupTemplate : MonoBehaviour
{
    private Powerup_DataType m_DataType;

    public Powerup_DataType DataType => m_DataType;
    [SerializeField] private Image m_Powerup_Image;
    [SerializeField] private Image m_Powerup_Rank_Image;
    [SerializeField] private Image m_RankPanel;
    [SerializeField] private TMPro.TextMeshProUGUI m_Powerup_Name;

    private float ColumnInterval;
    [SerializeField] private float ColumnPivot;
    [SerializeField] private float ColumnPadding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Powerup_DataType data)
    {
        m_DataType = data;
        m_Powerup_Image.sprite = data.Powerup_Image;
        m_Powerup_Name.text = data.Powerup_Name;
        float columnstart = -(m_RankPanel.GetComponent<RectTransform>().rect.width/2);
        ColumnInterval = m_Powerup_Rank_Image.GetComponent<RectTransform>().rect.width;
        int repeatcount = data.MaxRank;
        int checkcount = data.Rank;
        Image tempobject;
        for (int repeat = 0; repeat < repeatcount; ++repeat)
        {
            tempobject = Instantiate(m_Powerup_Rank_Image);
            tempobject.transform.SetParent(m_RankPanel.transform);
            tempobject.transform.localScale = m_Powerup_Rank_Image.transform.localScale;
            tempobject.transform.localPosition = new Vector3(columnstart + ColumnPivot + ((ColumnPadding + ColumnInterval) * repeat), 0f, 0f);
            if (repeat < checkcount)
            {
                tempobject.transform.Find("Check_Image").gameObject.SetActive(true);
            }
        }
    }

    public void OnClickPowerupTemplate()
    {
        EventSystem tempevent = EventSystem.current;
        if (tempevent != null)
        {
            GameObject tempobject = tempevent.currentSelectedGameObject;
            if (tempobject != null)
            {
                GameObject te = GameObject.Find("UnderPanel");
                te.GetComponent<UI_PowerupUnderPanel>().PowerupExplanInit(tempobject.GetComponent<UI_PowerupTemplate>().DataType);
            }
        }
    }
}
