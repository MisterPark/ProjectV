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
        return (obj == Player.Instance.gameObject);
    }

}
