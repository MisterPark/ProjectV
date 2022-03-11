using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Gameover : UI
{
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        GameManager.Instance.Pause();
        StartCoroutine("FadeOut");
    }

    public void OnClickDone()
    {
        SceneManager.LoadScene(UIManager.Instance.StartSceneName);
    }

    IEnumerator FadeOut()
    {
        float alpha = 0f;
        float startTime = Time.realtimeSinceStartup;
        float time;

        while(alpha < 1f)
        {
            image.color = new Color(0f, 0f, 0f, alpha);
            time = Time.realtimeSinceStartup - startTime;
            alpha = 0.5f * time;
            Debug.Log(time.ToString());
            yield return null;
        }

        image.color = new Color(0f, 0f, 0f, 1f);
        yield return null;
    }
}
