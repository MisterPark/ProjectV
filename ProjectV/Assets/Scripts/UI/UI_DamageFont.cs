using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamageFont : MonoBehaviourEx
{
    public float duration = 0.8f;
    public float speed = 1.3f;

    private float tick = 0f;
    private float alpha = 1f;
    [SerializeField] private Text text;
    [SerializeField] private Outline outline;

    public static Dictionary<GameObject, UI_DamageFont> UI_DamageFonts = new Dictionary<GameObject, UI_DamageFont>();

    protected override void Awake()
    {
        base.Awake();
        UI_DamageFonts.Add(gameObject, this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UI_DamageFonts.Remove(gameObject);
    }
    public static UI_DamageFont Find(GameObject obj)
    {
        UI_DamageFont damageFont;
        if (UI_DamageFonts.TryGetValue(obj, out damageFont) == false)
        {
            Debug.LogError("Unregistered damageFont");
        }
        return damageFont;
    }

    protected override void Start()
    {
        base.Start();
        Canvas temp = transform.parent.GetComponent<Canvas>();
        temp.worldCamera = Camera.main;
    }

    public override void FixedUpdateEx()
    {
        tick += Time.fixedDeltaTime;
        if (tick >= duration)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            tick = 0f;
            alpha = 1f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            ObjectPool.Instance.Free(transform.parent.gameObject);
        }

        float sin = Mathf.Sin(tick / duration * Mathf.PI);
        transform.localScale = Vector3.one * sin;
        alpha = sin;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public void Init(int value, Color fontColor, Color outlineColor, Vector3 position)
    {
        text.text = value.ToString();
        transform.position = position;
        text.color = fontColor;
        outline.effectColor = outlineColor;

        LookAtCamera();
    }

    private void LookAtCamera()
    {
        //transform.LookAt(Camera.main.transform.position);
        //transform.Rotate(0f, 180f, 0f);

        transform.forward = Camera.main.transform.forward;
    }
}
