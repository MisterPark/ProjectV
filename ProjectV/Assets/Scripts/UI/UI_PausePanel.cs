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
    }

    public override void Hide()
    {
        base.Hide();
        UI_StatusPanel.instance.Hide();
        UI_Option.instance.Hide();
        GameManager.Instance.Resume();
    }

    public void OnClickResume()
    {
        Hide();
    }

    public void OnClickOption()
    {
        //UIManager.Instance.SetUIActive("Option Panel", true);
        UI_Option.instance.Show();
    }

    public void OnClickCombine()
    {
        UI_CombinePanel.instance.Show();
    }
}
