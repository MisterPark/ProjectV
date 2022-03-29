using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PausePanel : UI
{
    public static UI_PausePanel instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Hide();
    }

    public override void Show()
    {
        if (UI_LevelUp.instance.Visible) return;
        GameManager.Instance.Pause();
        base.Show();
        UI_StatusPanel.instance.Show();
        UI_CombinePanel.instance.Show();
    }

    public override void Hide()
    {
        base.Hide();
        UI_CombinePanel.instance.Hide();
        UI_StatusPanel.instance.Hide();
        UI_Settings.instance.Hide();
        GameManager.Instance.Resume();
    }

    private void HidePanel()
    {
        UI_CombinePanel.instance.Hide();
        UI_Settings.instance.Hide();
    }
    public void OnClickResume()
    {
        Hide();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    public void OnClickOption()
    {

        SoundManager.Instance.PlaySFXSound("ShortButton");
        HidePanel();
        UI_Settings.instance.Show();
    }

    public void OnClickCombine()
    {
        SoundManager.Instance.PlaySFXSound("ShortButton");
        UI_Settings.instance.Hide();
        if (UI_CombinePanel.instance.Visible)
        {
            UI_CombinePanel.instance.Hide();
        }
        else
        {
            UI_CombinePanel.instance.Show();
        }

    }

    public void OnClickExit()
    {
        UI_ExitPanel.instance.Show();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
}
