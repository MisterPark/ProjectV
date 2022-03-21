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
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Hide();
    }

    public void OnClickYes()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void OnClickNo()
    {
        Hide();
    }
}
