using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_Title_SC : MonoBehaviourEx
{
    public string StartScene_Name;
    public EventSystem Event_Handle;
    [SerializeField]
    private Text Money_Text;
    private RectTransform screenRT;
    //private TMPro.TextMeshProUGUI Money_Text;

    
    protected override void Start()
    {
        base.Start();
        gameObject.Localized();
        UI_Settings.instance.OnClosed.AddListener(ShowCursor);
        screenRT = GetComponent<RectTransform>();
    }

    
    public override void UpdateEx()
    {
        Money_Text.text = DataManager.Instance.currentSaveData.currentGold.ToString();
        if (Event_Handle.sendNavigationEvents)
        {
            GameObject tempobject = Event_Handle.currentSelectedGameObject;
            if (tempobject == null)
            {
                Event_Handle.SetSelectedGameObject(Event_Handle.firstSelectedGameObject);
            }
        }
    }
    public void ShowCursor()
    {
        Event_Handle.SetSelectedGameObject(Event_Handle.firstSelectedGameObject);
    }
    public void OnClickStart()
    {
        transform.Find("Start Button").gameObject.SetActive(false);
        transform.Find("Powerup Button").gameObject.SetActive(false);
        transform.Find("SignIn Button").gameObject.SetActive(false);
   
        DataManager.Instance.Setting_PowerStat();
        UI_CharacterSelect.instance.Show();
        Event_Handle.SetSelectedGameObject(UI_CharacterSelect.instance.gameObject);
        //SceneManager.LoadScene(StartScene_Name);
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

    public void OnClickExit()
    {
        Transform tempobject = UI_Powerup.instacne.transform;
        Transform tempobject2 = UI_CharacterSelect.instance.transform;
        Transform tempobject3 = UI_StageSelect.instance.transform;
        Transform tempobject4 = UI_SignIn.instance.transform;
        if (tempobject.gameObject.activeSelf)
        {
            ShowCursor();
            tempobject.gameObject.SetActive(false);
            transform.Find("Start Button").gameObject.SetActive(true);
            transform.Find("Powerup Button").gameObject.SetActive(true);
            transform.Find("SignIn Button").gameObject.SetActive(true);
        }
        else if (tempobject2.gameObject.activeSelf)
        {
            ShowCursor();
            tempobject2.gameObject.SetActive(false);
            transform.Find("Start Button").gameObject.SetActive(true);
            transform.Find("Powerup Button").gameObject.SetActive(true);
            transform.Find("SignIn Button").gameObject.SetActive(true);
        }
        else if (tempobject3.gameObject.activeSelf)
        {
            ShowCursor();
            tempobject3.gameObject.SetActive(false);
            tempobject2.gameObject.SetActive(true);
        }
        else if(tempobject4.gameObject.activeSelf)
        {
            ShowCursor();
            tempobject4.gameObject.SetActive(false);
            transform.Find("Start Button").gameObject.SetActive(true);
            transform.Find("Powerup Button").gameObject.SetActive(true);
            transform.Find("SignIn Button").gameObject.SetActive(true);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
    
    public void OnClickOption()
    {
        UI_Settings.instance.Show();
        SoundManager.Instance.PlaySFXSound("ShortButton");
        Transform tempobject = UI_SignIn.instance.transform;
        if(tempobject.gameObject.activeSelf)
        {
            tempobject.gameObject.SetActive(false);
        }
    }
    public void OnClickLeaderBoard()
    {
        GPGSBinder.Inst.Login();
        GPGSBinder.Inst.ShowTargetLeaderboardUI(GPGSIds.leaderboard_kills);
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
    public void OnClickPowerup()
    {
        transform.Find("Start Button").gameObject.SetActive(false);
        transform.Find("Powerup Button").gameObject.SetActive(false);
        transform.Find("SignIn Button").gameObject.SetActive(false);
        UI_Powerup.instacne.Show();
        Event_Handle.SetSelectedGameObject(UI_Powerup.instacne.transform.Find("Reset_Button").gameObject);
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
    
    public void OnClickSignin()
    {
        //transform.Find("Start Button").gameObject.SetActive(false);
        //transform.Find("Powerup Button").gameObject.SetActive(false);
        //transform.Find("SignIn Button").gameObject.SetActive(false);
        UI_SignIn.instance.Show();
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }
}
