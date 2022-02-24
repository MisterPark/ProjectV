using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KillCount : MonoBehaviour
{
    public int value = 0;

    private RectTransform rectTransform;
    [SerializeField] private Text text;
    [SerializeField] private RectTransform imageRT;
    [SerializeField] private RectTransform parentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetSize();
        text.text = value.ToString();
    }

    void ResetSize()
    {
        Vector2 size = parentCanvas.sizeDelta;
        float height = size.y * 0.05f;
        float height004 = size.y * 0.04f;
        rectTransform.anchoredPosition = new Vector2(height * -7.5f, height * -1.8f);
        imageRT.sizeDelta = new Vector2(height004, height004);
        imageRT.anchoredPosition = new Vector2(height004, 0f);
        text.fontSize = ((int)height004);
    }
}
