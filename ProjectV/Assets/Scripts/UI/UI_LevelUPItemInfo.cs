using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUPItemInfo : UI
{
    public int placeNum = 1;
    private RectTransform rectTransform;
    private RectTransform parent;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform itemName;
    [SerializeField] private RectTransform index;
    [SerializeField] private RectTransform level;
    [SerializeField] private RectTransform icon;
    [SerializeField] private RectTransform button;
    private Text nameText;
    private Text descText;
    private Text levelText;
    private Image iconImage;
    private SkillKind kind;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        nameText = itemName.GetComponent<Text>();
        descText = index.GetComponent<Text>();
        levelText = level.GetComponent<Text>();
        parent = transform.parent.GetComponent<RectTransform>();
        iconImage = icon.GetComponent<Image>();
    }

    public void ResetSize()
    {
        float width = parent.sizeDelta.x;
        float height = parent.sizeDelta.y * 0.2f;
        rectTransform.anchoredPosition = new Vector2(0f, height * -placeNum);
        float size = height * 0.45f;
        rectTransform.sizeDelta = new Vector2(width, height);
        background.sizeDelta = new Vector2(width, height);
        button.sizeDelta = new Vector2(width, height);
        itemName.sizeDelta = new Vector2(width * 0.55f, size);
        index.sizeDelta = new Vector2(width, size);
        level.sizeDelta = new Vector2(width * 0.19f, size);
        icon.sizeDelta = new Vector2(size, size);
        nameText.fontSize = ((int)(size * 0.5f));
        descText.fontSize = ((int)(size * 0.5f));
        levelText.fontSize = ((int)(size * 0.5f));
    }

    public void Init(SkillKind skillKind, string skillName, string description, int skillLevel, Sprite skillIcon)
    {
        kind = skillKind;
        nameText.text = skillName;
        descText.text = description;
        if (skillLevel == 1)
            levelText.text = "New!!";
        else
            levelText.text = "LV." + skillLevel.ToString();
        iconImage.sprite = skillIcon;
    }

    public void OnClick()
    {
        UI_LevelUp.instance.OnSelect(kind);
    }

}
