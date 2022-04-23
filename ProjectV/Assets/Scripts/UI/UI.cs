using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviourEx, IFixedUpdater, IUpdater
{
    public bool Visible { get { return gameObject.activeSelf; } }


    protected override void OnEnable()
    {
        base.OnEnable();
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

    public virtual void FixedUpdateEx()
    {
        
    }

    public virtual void UpdateEx()
    {
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
