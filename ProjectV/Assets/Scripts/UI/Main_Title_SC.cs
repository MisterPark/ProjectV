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
    public Image Start_Image;
    public string StartScene_Name;
    public EventSystem Event_Handle;
    private int Money { get; set; }
    [SerializeField]
    private TMPro.TextMeshProUGUI Money_Text;

    // Start is called before the first frame update
    void Start()
    {
        Money_Text.text = Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
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
        SceneManager.LoadScene(StartScene_Name);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickOption()
    { }
    public void OnClickPowerup()
    { }

    public void OnKeyCursor()
    {
        Start_Image.color = Color.green;
    }
}
