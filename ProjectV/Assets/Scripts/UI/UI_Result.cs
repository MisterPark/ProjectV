using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Result : UI
{
    public static UI_Result instance;

    public Text surviveText;
    public Text goldText;
    public Text levelText;
    public Text killcountText;
    public Button doneButton;

  protected override void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        int total = (int)DataManager.Instance.currentGameData.totalPlayTime;
        int minute = total / 60;
        int second = total % 60;
        surviveText.text = $"{string.Format("{0:00}", minute)}:{string.Format("{0:00}", second)}";
        goldText.text = DataManager.Instance.currentGameData.gold.ToString();
        levelText.text = DataManager.Instance.currentGameData.playerLevel.ToString();
        killcountText.text = DataManager.Instance.currentGameData.killCount.ToString();
    }


    public void OnDoneButtonClick()
    {
        SaveDataManager.Instance.SaveGameData();
        SceneManager.LoadScene("TitleScene");
    }
}
