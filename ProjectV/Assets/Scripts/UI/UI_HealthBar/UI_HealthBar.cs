using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviourEx, IFixedUpdater
{
    [SerializeField] Image barImage;
    GameObject target;

    float Hp;
    float maxHp;
    float height = 0f;

    Camera cam;
    Canvas canvas;
    RectTransform canvasRect;
    [SerializeField] RectTransform bar;
    [SerializeField] RectTransform background;

    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
        target = Player.Instance.gameObject;
        if (target == null)
        {
            Debug.Log("Target not found");
        }
        else
        {
            height = target.GetComponent<CapsuleCollider>().height + 0.5f;
        }
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas not found");
        }
        else
        {
            canvasRect = canvas.GetComponent<RectTransform>();
            float horizontal = canvasRect.sizeDelta.x * 0.05f;
            Vector2 originBar = bar.sizeDelta;
            Vector2 originBackground = background.sizeDelta;
            float hRatio = horizontal / originBackground.x;
            background.sizeDelta = new Vector2(originBackground.x * hRatio, originBackground.y * hRatio);
            bar.sizeDelta = new Vector2(originBar.x * hRatio, originBar.y * hRatio);
        }
    }

    public void FixedUpdateEx()
    {
        float fillAmount = 0f;
        Hp = Player.Instance.stat.Get_FinalStat(StatType.Health);
        maxHp = Player.Instance.stat.Get_FinalStat(StatType.MaxHealth);
        if (Hp <= 0f)
        {
            fillAmount = 0f;
        }
        else
        {
            fillAmount = Hp / maxHp;
        }

        fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);

        barImage.fillAmount = fillAmount;
        //ransform.LookAt(Camera.main.transform);

        Vector3 worldPos = target.transform.position + new Vector3(0f, height, 0f);
        transform.position = cam.WorldToScreenPoint(worldPos);
    }
}
