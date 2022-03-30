using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Damaged : UI
{
    public static UI_Damaged instance;
    float tick = 0;
    float duration = 0.1f;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        Canvas canvas = transform.parent.GetComponent<Canvas>();
        if(canvas == null)
        {
            Debug.LogError("Canvas Not Found");
        }
        RectTransform canvasRect = canvas.transform.GetComponent<RectTransform>();
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = canvasRect.sizeDelta;
        Hide();
    }

    public override void FixedUpdateEx()
    {
        base.FixedUpdateEx();
        if (gameObject.activeSelf == false) return;
        tick += Time.fixedDeltaTime;
        if (tick > duration)
        {
            tick = 0;
            Hide();
        }
    }

    public override void Show()
    {
        base.Show();
        tick = 0;
    }
}
