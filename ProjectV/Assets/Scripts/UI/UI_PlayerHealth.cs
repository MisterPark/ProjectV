using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    public GameObject target;
    public Image barImage;

    private float Hp;
    private float maxHp;
    private float height = 0f;

    void Start()
    {
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

    void FixedUpdate()
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
