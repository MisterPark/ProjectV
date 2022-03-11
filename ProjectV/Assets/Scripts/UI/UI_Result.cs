using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Result : MonoBehaviour
{
    public Text surviveText;
    public Text goldText;
    public Text levelText;
    public Text killcountText;
    // Start is called before the first frame update
    void Start()
    {
        surviveText.text = DataManager.Instance.currentGameData.totalPlayTime.ToString(@"mm\:ss");
        goldText.text = DataManager.Instance.currentGameData.gold.ToString();
        killcountText.text = DataManager.Instance.currentGameData.killCount.ToString();
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(UIManager.Instance.StartSceneName);
        }
    }
}
