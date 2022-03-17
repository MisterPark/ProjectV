using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public bool Visible { get { return gameObject.activeSelf; } }
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Show(bool _visible)
    {
        if(_visible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
