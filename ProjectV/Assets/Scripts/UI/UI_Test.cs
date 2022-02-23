using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
            UI_DamageFont font = temp.transform.GetChild(0).GetComponent<UI_DamageFont>();
            font.Init(99, UI_DamageFont.FontColor.WHITE, transform.position + (Vector3.up * 2f));
        }
    }
}
