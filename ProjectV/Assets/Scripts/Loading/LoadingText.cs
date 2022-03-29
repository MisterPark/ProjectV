using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    Text text;
    string txt = "Loading...";

    float tick = 0;
    float time = 0.2f;
    int index = 0;
    void Start()
    {
        text = GetComponent<Text>();
    }

    
    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if(tick >= time)
        {
            tick = 0;
            index++;
            index = index >= txt.Length ? 0 : index;
        }

        text.text = txt.Substring(0, index + 1);
    }
}
