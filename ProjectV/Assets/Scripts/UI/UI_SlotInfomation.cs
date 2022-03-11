using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotInfomation : UI
{
    public Image icon;
    public Text text;

    public override void Show()
    {
        icon.color = new Color(1f, 1f, 1f, 1f);
        text.color = new Color(1f, 1f, 1f, 1f);
    }

    public override void Hide()
    {
        icon.color = new Color(1f, 1f, 1f, 0f);
        text.color = new Color(1f, 1f, 1f, 0f);
    }
}
