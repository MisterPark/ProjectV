using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Option : UI
{
    private RectTransform rectTransform;
    private RectTransform parent;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parent = transform.parent.GetComponent<RectTransform>();
        ResetSize();
        Hide();
    }

    public void OnClickExit()
    {
        Hide();
    }

    public void ResetSize()
    {
        float width = parent.sizeDelta.x;
        float height = parent.sizeDelta.y;
        float ratioX = 0.3f;
        float ratioY = 0.8f;
        rectTransform.sizeDelta = new Vector2(width * ratioX, height * ratioY);
    }
}
