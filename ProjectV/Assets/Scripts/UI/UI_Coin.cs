using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Coin : MonoBehaviour
{
    private RectTransform screenRT;
    private RectTransform imageRT;
    private RectTransform rectTransform;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        imageRT = transform.GetChild(0).GetComponent<RectTransform>();
        screenRT = transform.parent.GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
        text = transform.GetChild(1).GetComponent<Text>();
        ResetSize();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = DataManager.Instance.currentGameData.gold.ToString();
    }

    void ResetSize()
    {
        Vector2 size = screenRT.sizeDelta;
        float height = size.y * 0.05f;
        float height004 = size.y * 0.04f;
        rectTransform.anchoredPosition = new Vector2(height * -1.5f, height * -1.8f);
        imageRT.sizeDelta = new Vector2(height004, height004);
        imageRT.anchoredPosition = new Vector2(height004, 0f);
        text.fontSize = ((int)height004);
    }
}
