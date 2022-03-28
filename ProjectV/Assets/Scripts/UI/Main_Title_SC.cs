using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_Title_SC : MonoBehaviour
{
    public float KeyboardCursor_Xpos;
    public Image KeyboardCursor_Image;
    public string StartScene_Name;
    public EventSystem Event_Handle;
    [SerializeField]
    private Text Money_Text;
    private RectTransform screenRT;
    //private TMPro.TextMeshProUGUI Money_Text;

    // Start is called before the first frame update
    void Start()
    {
        UI_Settings.instance.OnClosed.AddListener(ShowCursor);
        screenRT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Money_Text.text = DataManager.Instance.currentSaveData.currentGold.ToString();
        if (Event_Handle.sendNavigationEvents)
        {
            GameObject tempobject = Event_Handle.currentSelectedGameObject;
            if (tempobject != null)
            {
                float objectwidth = (tempobject.GetComponent<RectTransform>().rect.width) / 4;
                Vector2 SelectedCursorPos = new Vector2(tempobject.transform.position.x - objectwidth, tempobject.transform.position.y);
                float width = screenRT.sizeDelta.x;
                if (tempobject.name=="Start Button")
                {
                    
                    KeyboardCursor_Image.transform.position = SelectedCursorPos + new Vector2(-width*10f, 0f);
                }
                else if(tempobject.name == "Powerup Button")
                {
                    KeyboardCursor_Image.transform.position = SelectedCursorPos + new Vector2(-width * 10f, 0f);
                }
                else if(tempobject.name == "Option Button")
                {
                    KeyboardCursor_Image.transform.position = SelectedCursorPos + new Vector2(-width * 8f, 0f);
                }
                else if (tempobject.name == "Exit Button")
                {
                    KeyboardCursor_Image.transform.position = SelectedCursorPos + new Vector2(-width * 5f, 0f);
                }
                else
                {
                    KeyboardCursor_Image.transform.position = SelectedCursorPos + new Vector2(-width * 10f, 0f);
                }
            }
            else
            {
                Event_Handle.SetSelectedGameObject(Event_Handle.firstSelectedGameObject);
            }
        }
    }
    public void ShowCursor()
    {
        Event_Handle.SetSelectedGameObject(Event_Handle.firstSelectedGameObject);
        KeyboardCursor_Image.gameObject.SetActive(true);
    }
    public void OnClickStart()
    {
        KeyboardCursor_Image.gameObject.SetActive(false);
        transform.Find("Start Button").gameObject.SetActive(false);
        transform.Find("Powerup Button").gameObject.SetActive(false);
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

        if (tempobject.gameObject.activeSelf)
        {
            ShowCursor();
            tempobject.gameObject.SetActive(false);
            transform.Find("Start Button").gameObject.SetActive(true);
            transform.Find("Powerup Button").gameObject.SetActive(true);
        }
        else if (tempobject2.gameObject.activeSelf)
        {
            ShowCursor();
            tempobject2.gameObject.SetActive(false);
            transform.Find("Start Button").gameObject.SetActive(true);
            transform.Find("Powerup Button").gameObject.SetActive(true);
        }
        else if (tempobject3.gameObject.activeSelf)
        {
            ShowCursor();
            tempobject3.gameObject.SetActive(false);
            tempobject2.gameObject.SetActive(true);
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
        KeyboardCursor_Image.gameObject.SetActive(false);
    }
    public void OnClickPowerup()
    {
        KeyboardCursor_Image.gameObject.SetActive(false);
        transform.Find("Start Button").gameObject.SetActive(false);
        transform.Find("Powerup Button").gameObject.SetActive(false);
        UI_Powerup.instacne.Show();
        Event_Handle.SetSelectedGameObject(UI_Powerup.instacne.transform.Find("Reset_Button").gameObject);
        SoundManager.Instance.PlaySFXSound("ShortButton");
    }

}
