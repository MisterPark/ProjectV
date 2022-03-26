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
}
