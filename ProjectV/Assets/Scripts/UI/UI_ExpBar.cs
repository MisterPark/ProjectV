using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExpBar : MonoBehaviour
{

    private float Exp;
    private float maxExp;
    private RectTransform rectTransform;
    private RectTransform barRT;
    private RectTransform backRT;
    private RectTransform parentCanvas;
    private Image barImage;
    // Start is called before the first frame update
    void Start()
    {
        barImage = transform.GetChild(1).GetComponent<Image>();
        barRT = transform.GetChild(1).GetComponent<RectTransform>();
        backRT = transform.GetChild(0).GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
        parentCanvas = transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Exp = Player.Instance.stat.Get_FinalStat(StatType.Exp);
        maxExp = Player.Instance.stat.Get_FinalStat(StatType.MaxExp);
        UpdateSize();
        barImage.fillAmount = Exp / maxExp;
    }

    void UpdateSize()
    {
        float width = parentCanvas.sizeDelta.x;
        float height = parentCanvas.sizeDelta.y * 0.05f;
        rectTransform.sizeDelta = new Vector2(0f, height);
        backRT.sizeDelta = new Vector2(width, height);
        barRT.sizeDelta = new Vector2(width - width * 0.023f, height * 0.73f);
    }
}
