using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviourEx
{
    public GameObject target;
    public Image barImage;

    private float Hp;
    private float maxHp;
    private float height = 0f;

    protected override void Start()
    {
        base.Start();
        target = Player.Instance.gameObject;
        if(target == null)
        {
            Debug.Log("Not Find Target");
        }
        else
        {
            height = target.GetComponent<CapsuleCollider>().height + 0.5f;
        }
            
    }

    public override void FixedUpdateEx()
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
        transform.LookAt(Camera.main.transform);
    }

    void LateUpdate()
    {
        transform.position = target.transform.position + new Vector3(0f, height, 0f);
    }
}
