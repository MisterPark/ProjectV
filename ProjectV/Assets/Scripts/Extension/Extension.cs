using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
