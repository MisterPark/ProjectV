using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UI_Settings : UI
{
    public static UI_Settings instance;
    private RectTransform rectTransform;
    private RectTransform parent;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Toggle damageNumberToggle;
    [SerializeField] Button prevLangButton;
    [SerializeField] Button nextLangButton;
    [SerializeField] Text langText;
    int langIndex = 0;

    public UnityEvent OnClosed = new UnityEvent();
    
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();

        bgmSlider.onValueChanged.AddListener(OnValueChange_BGM);
        soundSlider.onValueChanged.AddListener(OnValueChange_Sound);
        damageNumberToggle.onValueChanged.AddListener(OnValueChanged_VisibleDamageNumbers);

        bgmSlider.value = DataManager.Instance.Settings.BGMVolume;
        soundSlider.value = DataManager.Instance.Settings.SoundVolume;
        damageNumberToggle.isOn = DataManager.Instance.Settings.VisibleDamageNumbers;

        prevLangButton.onClick.AddListener(OnClickPrev);
        nextLangButton.onClick.AddListener(OnClickNext);

        UpdateLanguageText();
        Initialize();

        ResetSize();
        Hide();
    }

    public void Initialize()
    {
        bgmSlider.value = DataManager.Instance.Settings.BGMVolume;
        soundSlider.value = DataManager.Instance.Settings.SoundVolume;
        damageNumberToggle.isOn = DataManager.Instance.Settings.VisibleDamageNumbers;
    }

    public override void Show()
    {
        base.Show();

#if UNITY_STANDALONE
        Cursor.visible = true;
#endif
    }

    public override void Show(bool _visible)
    {
        base.Show(_visible);
#if UNITY_STANDALONE
        Cursor.visible = true;
#endif
    }
    public override void Hide()
    {
        base.Hide();
    }

    public void OnClickExit()
    {
        Hide();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    public void OnDisable()
    {
        OnClosed.Invoke();
    }

    public void ResetSize()
    {
        float width = parent.sizeDelta.x;
        float height = parent.sizeDelta.y;
        float ratioX = 0.5f;
        float ratioY = 0.6f;
        rectTransform.sizeDelta = new Vector2(width * ratioX, height * ratioY);
    }

    public void OnValueChanged_VisibleDamageNumbers(bool _isOn)
    {
        DataManager.Instance.Settings.VisibleDamageNumbers = _isOn;
    }

    public void OnValueChange_BGM(float _volume)
    {
        DataManager.Instance.Settings.BGMVolume = _volume;
        SoundManager.Instance.masterVolumeBGM = _volume;
        SoundManager.Instance.SetCurrentBgmVolume();
    }

    public void OnValueChange_Sound(float _volume)
    {
        DataManager.Instance.Settings.SoundVolume = _volume;
        SoundManager.Instance.masterVolumeSFX = _volume;
    }


    public void UpdateLanguageText()
    {
        Language language = DataManager.Instance.Settings.Language;
        langIndex = (int)language;
        langText.text = language.ToString();
    }

    public void OnClickPrev()
    {
        langIndex--;
        langIndex = langIndex < 0 ? 0 : langIndex;
        langIndex = langIndex >= (int)Language.End ? langIndex - 1 : langIndex;
        
        DataManager.Instance.Settings.Language = (Language)langIndex;
        UpdateLanguageText();
        DataManager.Instance.Localized();
    }

    public void OnClickNext()
    {
        langIndex++;
        langIndex = langIndex < 0 ? 0 : langIndex;
        langIndex = langIndex >= (int)Language.End ? langIndex - 1 : langIndex;

        DataManager.Instance.Settings.Language = (Language)langIndex;
        UpdateLanguageText();
        DataManager.Instance.Localized();
    }
}
