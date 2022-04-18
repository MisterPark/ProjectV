using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SignIn : UI
{
    string log;
    public static UI_SignIn instance;
    private RectTransform baseRect = null;
    private RectTransform canvasRect = null;

    private Canvas canvas = null;

    protected override void Awake()
    {


        
        base.Awake();
        instance = this;
    }

    
    protected override void Start()
    {
        base.Start();
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.transform.GetComponent<RectTransform>();
        baseRect.sizeDelta = new Vector2(canvasRect.sizeDelta.x, canvasRect.sizeDelta.y);
        Hide();
    }

    public override void Show(bool _visible)
    {
        base.Show(_visible);
    }

    public override void UpdateEx()
    {
    }

    public void OnClickSigninwithgoogle()
    {
        
            GPGSBinder.Inst.Login();
        
        Hide();
    }
    public void OnClickBackGround()
    {
        Hide();
    }
}
