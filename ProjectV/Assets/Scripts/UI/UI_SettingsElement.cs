using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SettingsType
{
    Slider,
    Toggle,
}

public class UI_SettingsElement : MonoBehaviour
{
    [SerializeField] SettingsType type;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
