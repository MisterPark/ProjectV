using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamageFont : MonoBehaviour
{
    public float time = 0.35f;
    public float speed = 1.3f;

    private float curTime = 0f;
    [SerializeField] private Text text;
    private RectTransform rectTransform;

    public enum FontColor { WHITE, RED }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        Canvas temp = transform.parent.GetComponent<Canvas>();
        temp.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= time)
        {
            transform.localPosition = Vector3.zero;
            curTime = 0f;
            ObjectPool.Instance.Free(transform.parent.gameObject);
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);
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
}
