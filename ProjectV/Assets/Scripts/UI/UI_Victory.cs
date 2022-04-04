using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Victory : UI
{
    public static UI_Victory instance;
    [SerializeField] Button doneButton;

    protected override void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        doneButton.onClick.AddListener(OnClickDone);
        Hide();
    }

    public void OnClickDone()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
