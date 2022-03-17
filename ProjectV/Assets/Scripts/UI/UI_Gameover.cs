using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Gameover : UI
{
    public static UI_Gameover instance;
    private Image image;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        image = GetComponent<Image>();
        Hide();
    }

    public override void Show()
    {
        base.Show();
        StartCoroutine(FadeOut());
    }

    public void OnClickDone()
    {
        SceneManager.LoadScene("ResultScene");
    }

    IEnumerator FadeOut()
    {
        float alpha = 0f;
        float startTime = Time.realtimeSinceStartup;
        float time;
        Color color = image.color;
        color.a = alpha;

        while(alpha < 0.5f)
        {
            color.a = alpha;
            image.color = color;
            time = Time.realtimeSinceStartup - startTime;
            alpha = 0.5f * time;
            yield return null;
        }

        color.a = 0.5f;
        image.color = color;
        yield return null;
    }
}
