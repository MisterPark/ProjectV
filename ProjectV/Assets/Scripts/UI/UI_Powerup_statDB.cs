using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupDB", menuName = "Data/PowerupDB")]
public class UI_Powerup_statDB : ScriptableObject
{
    [SerializeField] private List<Powerup_DataType> m_Powerup_Type_List;

    public int GetCount()
    {
        return Powerup_Type_List.Count;
    }

    public List<Powerup_DataType> Powerup_Type_List => m_Powerup_Type_List;
}
