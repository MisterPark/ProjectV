using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SkillInformation
{
    public SkillKind kind;
    public int nextLevel;
    public SkillInformation(SkillKind kind, int nextLevel)
    {
        this.kind = kind;
        this.nextLevel = nextLevel;
    }
}

public class UI_LevelUp : UI
{
    public static UI_LevelUp instance;

    public UI_LevelUPItemInfo[] children;

    private float ratioX = 0.34f;
    private float ratioY = 0.8f;
    private RectTransform rectTransform;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform outline;
    [SerializeField] private RectTransform mainText;
    private RectTransform parentCanvas;
    private Text text;
    public UnityEvent<SkillKind> OnSelected;

    int count = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        text = mainText.GetComponent<Text>();
        parentCanvas = transform.parent.GetComponent<RectTransform>();
        ResetSize();
        Clear();
        Hide();
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
        for (int i = 0; i < 4; i++)
        {
            children[i].ResetSize();
        }
    }

    public void Clear()
    {
        for (int i = 0; i < 4; i++)
        {
            children[i].Hide();
        }
    }

    public override void Show()
    {
        base.Show();
        Clear();
        for (int i = 0; i < count; i++)
        {
            children[i].Show();
        }
    }

    public override void Hide()
    {
        base.Hide();
        Clear();
    }

    public void SetSkillInfomations(List<SkillInformation> skills)
    {
        count = 0;
        foreach (SkillInformation item in skills)
        {
            SkillData data = DataManager.Instance.skillDatas[(int)item.kind].skillData;
            SkillValue value = data.values[(int)item.nextLevel-1];
            children[count].Init(item.kind, data.skillName, value.description, item.nextLevel, data.icon);
            count++;
        }
    }

    public void OnSelect(SkillKind kind)
    {
        OnSelected?.Invoke(kind);
        transform.gameObject.SetActive(false);
    }
}
