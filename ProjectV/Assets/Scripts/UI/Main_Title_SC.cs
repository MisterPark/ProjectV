using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Title_SC : MonoBehaviour
{
    public Image Start_Image;
    public string StartScene_Name;
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
