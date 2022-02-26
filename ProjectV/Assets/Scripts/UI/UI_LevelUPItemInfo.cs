using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUPItemInfo : MonoBehaviour
{
    public int placeNum = 1;
    private RectTransform rectTransform;
    [SerializeField] private RectTransform parentUI;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform itemName;
    [SerializeField] private RectTransform index;
    [SerializeField] private RectTransform level;
    [SerializeField] private RectTransform icon;
    private Text nameText;
    private Text indexText;
    private Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        nameText = itemName.GetComponent<Text>();
        indexText = index.GetComponent<Text>();
        levelText = level.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetSize();
    }

    private void ResetSize()
    {
        float width = parentUI.sizeDelta.x;
        float height = parentUI.sizeDelta.y * 0.2f;
        rectTransform.anchoredPosition = new Vector2(0f, height * -placeNum);
        float size = height * 0.45f;
        rectTransform.sizeDelta = new Vector2(width, height);
        background.sizeDelta = new Vector2(width, height);
        itemName.sizeDelta = new Vector2(width * 0.55f, size);
        index.sizeDelta = new Vector2(width, size);
        level.sizeDelta = new Vector2(width * 0.19f, size);
        icon.sizeDelta = new Vector2(size, size);
        nameText.fontSize = ((int)(size * 0.5f));
        indexText.fontSize = ((int)(size * 0.5f));
        levelText.fontSize = ((int)(size * 0.5f));
    }

    //public void Init(int num)
    //{
    //    placeNum = num;
    //    parentRectTransform = transform.parent.GetComponent<RectTransform>();
    //}
}
