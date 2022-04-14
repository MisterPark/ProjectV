using System;
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
        int kills = DataManager.Instance.currentGameData.killCount;
        if (kills >= 1000)
        {
            GPGSBinder.Inst.UnlockAchievement(GPGSIds.achievement_1000_kills);
        }
        if (kills>=10000)
        {
            GPGSBinder.Inst.UnlockAchievement(GPGSIds.achievement_10000_kills);
        }

        //if (Convert.ToInt32(GPGSIds.leaderboard_kills) < kills)
        //{
        //}
        //GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_kills, 1);
        GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_kills, (long)kills);

    }


    public void OnDoneButtonClick()
    {
        SaveDataManager.Instance.SaveGameData();
        SceneManager.LoadScene("TitleScene");
    }
}
