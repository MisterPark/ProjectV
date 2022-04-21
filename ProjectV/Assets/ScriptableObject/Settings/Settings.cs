using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Data/Settings")]
public class Settings : ScriptableObject
{
    public float BGMVolume;
    public float SoundVolume;
    public bool VisibleDamageNumbers;
    public Language Language;
    public bool shadowFlag;
}

[System.Serializable]
public class SettingsData
{
    public float BGMVolume = 1f;
    public float SoundVolume = 1f;
    public bool VisibleDamageNumbers = true;
    public Language Language = Language.English;
    public bool shadowFlag = true;
}
