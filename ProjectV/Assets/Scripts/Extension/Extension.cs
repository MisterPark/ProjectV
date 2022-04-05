using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public static class Extension
{
    /// <summary>
    /// 플레이어인지 체크합니다.
    /// </summary>
    public static bool IsPlayer(this GameObject obj)
    {
        if (obj == null) return false;
        if (Player.Instance == null) return false;
        return (obj == Player.Instance.gameObject);
    }
    /// <summary>
    /// 플레이어와의 거리를 리턴합니다. 
    /// </summary>
    /// <returns> Null인 경우 float.MaxValue를 반환합니다.</returns>
    public static float GetDistanceFromPlayer(this GameObject obj)
    {
        if (obj == null) return float.MaxValue;
        if (Player.Instance == null) return float.MaxValue;

        return (Player.Instance.transform.position - obj.transform.position).magnitude;
    }

    public static bool Dequeue<T>(this List<T> list, out T data)
    {
        data = default(T);
        if (list == null) return false;
        if (list.Count == 0) return false;

        data = list[0];
        list.RemoveAt(0);
        return true;

    }

    public static SkillType GetSkillType(this SkillKind kind)
    {
        return DataManager.Instance.skillDatas[(int)kind].skillData.type;
    }

    public static SkillKind GetRandomSkillKind()
    {
        return (SkillKind)Random.Range((int)SkillKind.IceBolt, ((int)SkillKind.End) - 1);
    }

    public static int ToInt(this SkillLevel level)
    {
        return (int)level + 1;
    }

    public static SkillLevel ToSkillLevel(this int level)
    {
        return (SkillLevel)(level - 1);
    }

    public class Less : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((new CaseInsensitiveComparer()).Compare(x, y));
        }
    }

    public static string Localized(this string text)
    {
        if (DataManager.Instance == null) return text;

        Language language = DataManager.Instance.Settings.Language;
        var datas = DataManager.Instance.Localization.datas;
        LocalizationElement elem = null;
        int count = datas.Length;
        for(int i = 0; i < count; i++)
        {
            var data = datas[i];
            if(text == data.English ||
                text == data.Korean)
            {
                elem = data;
                break;
            }
        }

        if(elem == null)
        {
            return text;
        }

        switch (language)
        {
            case Language.English: return elem.English;
            case Language.Korean: return elem.Korean;

        }

        return text;
    }

    public static string Localized(this string text, Language language)
    {
        if (DataManager.Instance == null) return text;

        var datas = DataManager.Instance.Localization.datas;
        LocalizationElement elem = null;
        int count = datas.Length;
        for (int i = 0; i < count; i++)
        {
            var data = datas[i];
            if (text == data.English ||
                text == data.Korean)
            {
                elem = data;
                break;
            }
        }

        if (elem == null)
        {
            return text;
        }

        switch (language)
        {
            case Language.English: return elem.English;
            case Language.Korean: return elem.Korean;

        }

        return text;
    }

    public static void Localized(this GameObject gameObject)
    {
        Text text = gameObject.GetComponent<Text>();
        if (text != null)
        {
            text.text = text.text.Localized();
        }

        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        for (int j = 0; j < texts.Length; j++)
        {
            texts[j].text = texts[j].text.Localized();
        }
    }

    public static string ToString(this Language language)
    {
        switch (language)
        {
            case Language.English: return "English";
            case Language.Korean: return "Korean";
            case Language.End:
                break;
            default:
                break;

        }
        return string.Empty;
    }
}


