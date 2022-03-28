using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PowerupTemplate : UI
{
    private Powerup_DataType m_DataType;

    public Powerup_DataType DataType => m_DataType;
    [SerializeField] private Image m_Powerup_Image;
    [SerializeField] private Image m_Powerup_Rank_Image;
    [SerializeField] private Image m_RankPanel;
    [SerializeField] private Text m_Powerup_Name;
    private List<GameObject> RankImageList = new List<GameObject>();

    private float ColumnInterval;
    [SerializeField] private float ColumnPivot;
    [SerializeField] private float ColumnPadding;

    private int MaxRankCount;
    private int RankCount;
    
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
        MaxRankCount = data.MaxRank;
        RankCount = data.Rank;
        Image tempobject;
        for (int repeat = 0; repeat < MaxRankCount; ++repeat)
        {
            tempobject = Instantiate(m_Powerup_Rank_Image);
            RankImageList.Add(tempobject.gameObject);
            tempobject.transform.SetParent(m_RankPanel.transform);
            tempobject.transform.localScale = m_Powerup_Rank_Image.transform.localScale;
            tempobject.transform.localPosition = new Vector3(columnstart + ColumnPivot + ((ColumnPadding + ColumnInterval) * repeat), 0f, 0f);
            if (repeat < RankCount)
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
                GameObject te = GameObject.Find("Powerup_Panel");
                te.GetComponent<UI_Powerup>().PowerupExplanInit(tempobject.GetComponent<UI_PowerupTemplate>().DataType);
            }
        }
    }

    public void ResetRankImage()
    {
        RankCount = DataType.Rank;
        for (int repeat = 0; repeat < MaxRankCount; ++repeat)
        {
            if (repeat < RankCount)
            {
                RankImageList[repeat].transform.Find("Check_Image").gameObject.SetActive(true);
            }
            else
            {
                RankImageList[repeat].transform.Find("Check_Image").gameObject.SetActive(false);
            }
        }
    }
}
