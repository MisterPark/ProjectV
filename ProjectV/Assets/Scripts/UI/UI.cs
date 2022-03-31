using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviourEx
{
    public bool Visible { get { return gameObject.activeSelf; } }


    protected virtual void OnEnable()
    {
        gameObject.Localized();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void FixedUpdateEx()
    {
        base.FixedUpdateEx();
    }

    public override void UpdateEx()
    {
        base.UpdateEx();
    }

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
