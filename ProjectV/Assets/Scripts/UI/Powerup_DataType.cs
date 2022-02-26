using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Powerup_DataType", menuName = "Powerup_DataType")]
public class Powerup_DataType : ScriptableObject
{
    [SerializeField] private StatType m_PowerType;
    [SerializeField] private string m_Powerup_Name;
    [SerializeField] private Sprite m_Powerup_Image;
    [SerializeField] private string m_Powerup_Tip;
    [SerializeField] private int m_MaxRank;
    [SerializeField] private int m_Rank;
    [SerializeField] private bool m_IsPercentage;
    [SerializeField] private float m_Powerup_Value;
    [SerializeField] private int m_Powerup_Price;

    public StatType PowerType => m_PowerType;
    public string Powerup_Name => m_Powerup_Name;
    public Sprite Powerup_Image => m_Powerup_Image;
    public string Powerup_Tip => m_Powerup_Tip;
    public int MaxRank => m_MaxRank;
    public int Rank => m_Rank;
    public bool IsPercentage => m_IsPercentage;
    public float Powerup_Value => m_Powerup_Value;
    public int Powerup_Price => m_Powerup_Price;

    public void SetRank(int ivalue)
    {
        if (ivalue <= m_MaxRank)
        {
            m_Rank = ivalue;
        }
    }

    private void PriceIncrease()
    {
        
    }
}
