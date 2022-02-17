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
        return (obj == Player.Instance.gameObject);
    }

}
