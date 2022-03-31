using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_ExitPanel : UI
{
    public static UI_ExitPanel instance;

    private RectTransform rectTransform;
    private RectTransform parent;
  protected override void Awake()
    {
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        Hide();
    }

    public void OnClickYes()
    {
        SoundManager.Instance.PlaySFXSound("ShortButton");
        SceneManager.LoadScene("ResultScene");
    }

    public void OnClickNo()
    {
        SoundManager.Instance.PlaySFXSound("ShortButton");
        Hide();
    }
}
