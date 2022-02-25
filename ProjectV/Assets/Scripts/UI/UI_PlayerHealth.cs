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
    private float height = 0f;
    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance.gameObject;
        if(target == null)
        {
            Debug.Log("Not Find Target");
        }
        else 
            height = target.GetComponent<CapsuleCollider>().height;
    }

    void Update()
    {
        Hp = tempHp;
        maxHp = tempMaxHp;
        barImage.fillAmount = Hp / maxHp;
        transform.LookAt(Camera.main.transform);
    }

    void LateUpdate()
    {
        transform.position = target.transform.position + new Vector3(0f, height, 0f);
    }
}
