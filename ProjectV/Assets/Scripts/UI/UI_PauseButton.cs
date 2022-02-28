using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseButton : MonoBehaviour
{
    private float ratio;
    private RectTransform rectTransform;
    private RectTransform parent;
    private RectTransform button;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();
        if (parent == null)
            Debug.Log("부모가 없으면 작동하지 않습니다.");
        button = transform.GetChild(0).GetComponent<RectTransform>();
        ratio = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        ResetSize();
    }

    private void ResetSize()
    {
        float width = parent.sizeDelta.x * ratio;
        float height = parent.sizeDelta.y * ratio;
        rectTransform.sizeDelta = new Vector2(width, width);
        button.sizeDelta = new Vector2(width, width);
        rectTransform.anchoredPosition = new Vector2(width * -0.1f , height * -2.5f);
    }
}
