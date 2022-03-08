using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayTime : MonoBehaviour
{
    private int second = 0;
    private int minute = 0;
    private float curTime = 0f;

    private RectTransform rectTransform;
    [SerializeField] Text text;
    private RectTransform parentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = transform.parent.GetComponent<RectTransform>();
        ResetSize();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = DataManager.Instance.currentGameData.totalPlayTime.ToString(@"mm\:ss");
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
