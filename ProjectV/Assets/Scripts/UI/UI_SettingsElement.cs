using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SettingsType
{
    Slider,
    Toggle,
}

public class UI_SettingsElement : MonoBehaviourEx
{
    [SerializeField] SettingsType type;
    protected override void Start()
    {
        base.Start();
    }

    
    public override void UpdateEx()
    {
        
    }
}
