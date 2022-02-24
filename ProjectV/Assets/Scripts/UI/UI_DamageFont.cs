using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamageFont : MonoBehaviour
{
    public float time = 0.8f;
    public float speed = 1.3f;

    private float curTime = 0f;
    private float alpha = 1f;
    [SerializeField] private Text text;
    [SerializeField] private Outline outline;

    public enum FontColor { WHITE, RED }

    //생성할때 코드
    //GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
    //UI_DamageFont font = temp.transform.GetChild(0).GetComponent<UI_DamageFont>();
    //font.Init(99, UI_DamageFont.FontColor.WHITE);

    // Start is called before the first frame update
    void Start()
    {
        Canvas temp = transform.parent.GetComponent<Canvas>();
        temp.worldCamera = Camera.main;
        text.fontSize = 50;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= time)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            curTime = 0f;
            alpha = 1f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            ObjectPool.Instance.Free(transform.parent.gameObject);
        }
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
        if(curTime <= time * 0.4f)
        {
            transform.localScale += Vector3.one * 0.6f * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * 0.8f * Time.deltaTime;
            alpha -= 4f * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
    }

    public void Init(int value, FontColor color , Vector3 position)
    {
        text.text = value.ToString();
        transform.position = position;
        switch(color)
        {
            case FontColor.WHITE:
                text.color = Color.white;
                outline.effectColor = Color.black;
                break;
            case FontColor.RED:
                text.color = Color.red;
                outline.effectColor = Color.black;
                break;
            default:
                text.color = Color.white;
                outline.effectColor = Color.black;
                break;
        }
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0f, 180f, 0f);
    }
}
