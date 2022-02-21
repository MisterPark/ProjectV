using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    public float tempHp;
    public float tempMaxHp;
    public GameObject target;
    public Image barImage;

    private float Hp;
    private float maxHp;

    void Start()
    {

    }

    void Update()
    {
        if (target == null)
            return;
        Hp = tempHp;
        maxHp = tempMaxHp;
        barImage.fillAmount = Hp / maxHp;
        transform.position = target.transform.position + new Vector3(0f, target.GetComponent<CapsuleCollider>().height, 0f);
        transform.LookAt(Camera.main.transform);
    }
}
