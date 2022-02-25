using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    /// <summary>
    /// �÷��̾����� üũ�մϴ�.
    /// </summary>
    public static bool IsPlayer(this GameObject obj)
    {
        if (obj == null) return false;
        if (Player.Instance == null) return false;
        return (obj == Player.Instance.gameObject);
    }
    /// <summary>
    /// �÷��̾���� �Ÿ��� �����մϴ�. 
    /// </summary>
    /// <returns> Null�� ��� float.MaxValue�� ��ȯ�մϴ�.</returns>
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
