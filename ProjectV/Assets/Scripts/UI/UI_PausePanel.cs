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
    }

    public void OnClickOption()
    {

    }
}
