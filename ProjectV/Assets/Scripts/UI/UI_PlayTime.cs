using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayTime : MonoBehaviourEx
{
    private RectTransform rectTransform;
    [SerializeField] Text text;
    private RectTransform parentCanvas;
    
    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = transform.parent.GetComponent<RectTransform>();
        ResetSize();
    }

    
    public override void UpdateEx()
    {
        int total = (int)DataManager.Instance.currentGameData.totalPlayTime;
        int minute = total / 60;
        int second = total % 60;
        text.text = $"{string.Format("{0:00}",minute)}:{string.Format("{0:00}", second)}";
    }

    void ResetSize()
    {
        Vector2 size = parentCanvas.sizeDelta;
        float height = size.y * 0.05f;
        float height006 = size.y * 0.06f;
        rectTransform.anchoredPosition = new Vector2(0f, height * -1.8f);
        text.fontSize = ((int)height006);
    }
}
