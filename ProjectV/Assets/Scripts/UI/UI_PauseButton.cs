using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseButton : MonoBehaviourEx
{
    public static UI_PauseButton instance;
    private float ratio;
    private RectTransform rectTransform;
    private RectTransform parent;
    private RectTransform button;

    protected override void Awake()
    {
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();
        button = transform.GetChild(0).GetComponent<RectTransform>();
        ratio = 0.05f;
        ResetSize();
    }

    
    public override void UpdateEx()
    {
    }

    private void ResetSize()
    {
        float width = parent.sizeDelta.x * ratio;
        float height = parent.sizeDelta.y * ratio;
        rectTransform.sizeDelta = new Vector2(width, width);
        button.sizeDelta = new Vector2(width, width);
        rectTransform.anchoredPosition = new Vector2(width * -0.1f , height * -2.5f);
    }

    public void OnClickPause()
    {
        UI_PausePanel.instance.Show();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
}
