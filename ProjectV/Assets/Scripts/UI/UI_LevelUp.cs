using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_LevelUp : MonoBehaviour
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
    public UnityEvent<SkillKind> callback;

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
        Player.Instance.OnLevelUp.AddListener(LevelupCallback);
        transform.gameObject.SetActive(false);
    }

    void Update()
    {
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
        for(int i=0; i <4; i++)
        {
            children[i].ResetSize();
        }
    }
    public void Init()
    {
        transform.gameObject.SetActive(true);
        
    }

    public void OnCallback(SkillKind kind)
    {
        callback?.Invoke(kind);
        transform.gameObject.SetActive(false);
    }

    public void LevelupCallback(int level)
    {
        Init();
    }
}
