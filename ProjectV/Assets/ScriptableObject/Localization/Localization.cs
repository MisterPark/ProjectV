using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization Data", menuName = "Data/Localization Data"), System.Serializable]
public class Localization : ScriptableObject
{
#if UNITY_EDITOR
    [ArrayElementTitle("English")]
#endif
    public LocalizationElement[] datas = new LocalizationElement[1];

}

[System.Serializable]
public enum Language
{
    English,
    Korean,
    End
}

[System.Serializable]
public class LocalizationElement : IComparable
{
    public string English;
    public string Korean;

    public int CompareTo(object obj)
    {
        LocalizationElement other = (LocalizationElement)obj;
        if (other == null) return -1;

        return this.English.CompareTo(other.English);
    }
}