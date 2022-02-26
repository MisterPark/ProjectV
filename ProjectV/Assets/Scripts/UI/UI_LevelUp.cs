using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUp : MonoBehaviour
{
    public GameObject prefab;
    private UI_LevelUPItemInfo prefabInfo;
    private int count = 1;

    private float ratioX = 0.34f;
    private float ratioY = 0.8f;
    private RectTransform rectTransform;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform outline;
    [SerializeField] private RectTransform mainText;
    [SerializeField] private RectTransform parentCanvas;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        text = mainText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetSize();
    }

    private void ResetSize()
    {
        float width = parentCanvas.sizeDelta.x * ratioX;
        float height = parentCanvas.sizeDelta.y * ratioY;
        float outlineSize = (parentCanvas.sizeDelta.x * (ratioX + 0.01f)) - (parentCanvas.sizeDelta.x * ratioX);
        rectTransform.sizeDelta = new Vector2(width, height);
        outline.sizeDelta = new Vector2(width + outlineSize, height + outlineSize);
        background.sizeDelta = new Vector2(width, height);
        mainText.sizeDelta = new Vector2(width, height * 0.2f);
        text.fontSize = ((int)(height * 0.1f));
    }

    public void Init()
    {

    }
}
