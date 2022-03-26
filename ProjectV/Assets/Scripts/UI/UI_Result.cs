using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Result : UI
{
    public Text surviveText;
    public Text goldText;
    public Text levelText;
    public Text killcountText;
    public Button doneButton;

    void Start()
    {
        int total = (int)DataManager.Instance.currentGameData.totalPlayTime;
        int minute = total / 60;
        int second = total % 60;
        surviveText.text = $"{string.Format("{0:00}", minute)}:{string.Format("{0:00}", second)}";
        goldText.text = DataManager.Instance.currentGameData.gold.ToString();
        killcountText.text = DataManager.Instance.currentGameData.killCount.ToString();
    }


    public void OnDoneButtonClick()
    {
        SaveDataManager.Instance.SaveGameData();
        SceneManager.LoadScene("TitleScene");
    }
}
