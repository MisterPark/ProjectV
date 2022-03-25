using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : UI
{
    public static UI_Settings instance;
    private RectTransform rectTransform;
    private RectTransform parent;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Toggle damageNumberToggle;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();

        bgmSlider.onValueChanged.AddListener(OnValueChange_BGM);
        soundSlider.onValueChanged.AddListener(OnValueChange_Sound);
        damageNumberToggle.onValueChanged.AddListener(OnValueChanged_VisibleDamageNumbers);

        ResetSize();
        Hide();
    }

    public void OnClickExit()
    {
        Hide();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    public void ResetSize()
    {
        float width = parent.sizeDelta.x;
        float height = parent.sizeDelta.y;
        float ratioX = 0.3f;
        float ratioY = 0.8f;
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
    }

    public void OnValueChange_Sound(float _volume)
    {
        DataManager.Instance.Settings.SoundVolume = _volume;
        SoundManager.Instance.masterVolumeSFX = _volume;
    }
}
