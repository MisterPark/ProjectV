using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Arrow : MonoBehaviour
{
    public Image Arrow_Image;
    private float Twinkle_Time;
    private float Twinkle_Speed;
    
    void Start()
    {
        Twinkle_Speed = 100f;
    }

    
    void Update()
    {
        Twinkle_Time += Time.deltaTime;
        if (Twinkle_Time < 0.1f)
        {
            Arrow_Image.transform.localPosition += new Vector3(Twinkle_Speed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            Twinkle_Time = 0f;
            Twinkle_Speed *= -1;
        }
    }
}
