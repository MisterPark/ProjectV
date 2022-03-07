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
    [SerializeField] RectTransform level;
    private Image barImage;
    private Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        barImage = transform.GetChild(1).GetComponent<Image>();
        barRT = transform.GetChild(1).GetComponent<RectTransform>();
        backRT = transform.GetChild(0).GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
        parentCanvas = transform.parent.GetComponent<RectTransform>();
        levelText = level.GetComponent<Text>();
        UpdateSize();
    }

    // Update is called once per frame
    void Update()
    {
        Exp = Player.Instance.stat.Get_FinalStat(StatType.Exp);
        maxExp = Player.Instance.stat.Get_FinalStat(StatType.MaxExp); 
        barImage.fillAmount = Exp / maxExp;
        levelText.text = "LV." + Player.Instance.stat.Get_FinalStat(StatType.Level).ToString();
    }

    void UpdateSize()
    {
        float width = parentCanvas.sizeDelta.x;
        float height = parentCanvas.sizeDelta.y * 0.05f;
        float barHeight = height * 0.73f;
        rectTransform.sizeDelta = new Vector2(width, height);
        backRT.sizeDelta = new Vector2(width, height);
        barRT.sizeDelta = new Vector2(width - width * 0.023f, barHeight);
        level.sizeDelta = new Vector2(200f, height);
        levelText.fontSize = (int)height;
    }
}
