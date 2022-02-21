using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExpBar : MonoBehaviour
{
    public float tempExp = 0f;
    public float tempMaxExp = 100f;

    private float Exp;
    private float maxExp;
    private RectTransform rectTransform;
    private RectTransform screenRT;
    private RectTransform barRT;
    private RectTransform backRT;
    private Image barImage;
    // Start is called before the first frame update
    void Start()
    {
        barImage = transform.GetChild(1).GetComponent<Image>();
        barRT = transform.GetChild(1).GetComponent<RectTransform>();
        backRT = transform.GetChild(0).GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
        screenRT = transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Exp = tempExp;
        maxExp = tempMaxExp;
        UpdateSize();
        barImage.fillAmount = Exp / maxExp;
    }

    void UpdateSize()
    {
        Vector2 screen = screenRT.sizeDelta;
        float width = screen.x;
        float height = screen.y * 0.05f;
        rectTransform.sizeDelta = new Vector2(0f, height);
        backRT.sizeDelta = new Vector2(width, height);
        barRT.sizeDelta = new Vector2(width - width * 0.023f, height * 0.73f);
    }
}
