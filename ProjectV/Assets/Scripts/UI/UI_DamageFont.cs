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
    private RectTransform rectTransform;

    public enum FontColor { WHITE, RED }

    //생성할때 코드
    //GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
    //temp.transform.position = transform.position + (Vector3.up* 2f); ->나타날 위치(콜라이더 충돌 지점 주면 좋을듯)
    //UI_DamageFont font = temp.transform.GetChild(0).GetComponent<UI_DamageFont>();
    //font.Init(99, UI_DamageFont.FontColor.WHITE); (나타날 숫자와 색상 현재 흰색 빨간색 밖에 없음)

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        Canvas temp = transform.parent.GetComponent<Canvas>();
        temp.worldCamera = Camera.main;
        text.fontSize = 50;
        LookAtCamera();
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

    public void Init(int value, FontColor color)
    {
        text.text = value.ToString();
        switch(color)
        {
            case FontColor.WHITE:
                text.color = Color.white;
                break;
            case FontColor.RED:
                text.color = Color.red;
                break;
            default:
                text.color = Color.white;
                break;
        }
    }

    private void LookAtCamera()
    {
        Vector3 camera = Camera.main.transform.position;
        Vector3 pos = transform.position;
        transform.LookAt(new Vector3(camera.x, camera.y, camera.z));
        transform.Rotate(0f, 180f, 0f);
    }
}
