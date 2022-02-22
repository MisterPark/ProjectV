using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Test : MonoBehaviour
{
    public Transform parentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
            //temp.transform.SetParent(parentCanvas);
            temp.transform.position = transform.position + (Vector3.up * 2f);
            UI_DamageFont font = temp.transform.GetChild(0).GetComponent<UI_DamageFont>();
            font.Init(99, UI_DamageFont.FontColor.WHITE);
        }
        if (Input.GetKey(KeyCode.K))
        {
            GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
            //temp.transform.SetParent(parentCanvas);
            temp.transform.position = transform.position + (Vector3.up * 2f);
            UI_DamageFont font = temp.transform.GetChild(0).GetComponent<UI_DamageFont>();
            font.Init(100, UI_DamageFont.FontColor.RED);
        }
    }
}
