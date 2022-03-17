using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PausePanel : UI
{
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    public void OnClickResume()
    {
        GameManager.Instance.Resume();
        Hide();
        UIManager.Instance.SetUIActive("Status Panel", false);
        if (UIManager.Instance.GetUIActive("Option Panel"))
            UIManager.Instance.SetUIActive("Option Panel", false);
    }

    public void OnClickOption()
    {
        UIManager.Instance.SetUIActive("Option Panel", true);
    }

    public void OnClickCombine()
    {
        UIManager.Instance.SetUIActive("Combine Panel", true);
    }
}
