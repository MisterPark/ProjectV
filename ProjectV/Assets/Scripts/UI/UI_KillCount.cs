using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KillCount : MonoBehaviourEx
{
    private RectTransform rectTransform;
    [SerializeField] private Text text;
    [SerializeField] private RectTransform imageRT;
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
        text.text = DataManager.Instance.currentGameData.killCount.ToString();
    }

    void ResetSize()
    {
        Vector2 size = parentCanvas.sizeDelta;
        float height = size.y * 0.05f;
        float height004 = size.y * 0.04f;
        rectTransform.anchoredPosition = new Vector2(height * -7.5f, height * -1.8f);
        imageRT.sizeDelta = new Vector2(height004, height004);
        imageRT.anchoredPosition = new Vector2(height004, 0f);
        text.fontSize = ((int)(height004*1.5f));
    }
}
