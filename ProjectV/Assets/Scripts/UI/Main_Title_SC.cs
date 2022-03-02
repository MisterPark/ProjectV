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
    private TMPro.TextMeshProUGUI Money_Text;

    // Start is called before the first frame update
    void Start()
    {
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
                KeyboardCursor_Image.transform.position = SelectedCursorPos;
            }
            else
            {
                Event_Handle.SetSelectedGameObject(Event_Handle.firstSelectedGameObject);
            }
        }
    }

    public void OnClickStart()
    {
        DataManager.Instance.Setting_PowerStat();
        SceneManager.LoadScene(StartScene_Name);
    }

    public void OnClickExit()
    {
        Transform tempobject = UIManager.Instance.GetUIObject("Powerup_Panel").transform;
        if (tempobject.gameObject.activeSelf)
        {
            tempobject.gameObject.SetActive(false);
            transform.Find("Start_Button").gameObject.SetActive(true);
            transform.Find("Powerup_Button").gameObject.SetActive(true);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }

    public void OnClickOption()
    { }
    public void OnClickPowerup()
    {
        KeyboardCursor_Image.gameObject.SetActive(false);
        transform.Find("Start_Button").gameObject.SetActive(false);
        transform.Find("Powerup_Button").gameObject.SetActive(false);
        UIManager.Instance.SetUIActive("Powerup_Panel", true);
        Event_Handle.SetSelectedGameObject(UIManager.Instance.GetUIObject("Powerup_Panel").transform.Find("Reset_Button").gameObject);
    }

}
