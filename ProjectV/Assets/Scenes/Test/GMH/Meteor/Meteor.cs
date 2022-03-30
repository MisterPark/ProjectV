using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviourEx
{
    
    bool createCrater = false;
    GameObject meteor;
    Missile parentMissile;
    protected override void Start()
    {
        base.Start();
        createCrater = false;
        
    }

    private void OnEnable()
    {
        if (null == meteor)
        {
            meteor = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject; 
        }
        if (parentMissile == null)
        {
            parentMissile = GetComponent<Missile>();
        }
        createCrater = false;
        meteor.SetActive(true);
    }
    
    public override void FixedUpdateEx()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Board")
        {
            Debug.Log("Ãæµ¹");
            if (!createCrater)
            {
                GameObject obj = ObjectPool.Instance.Allocate("MeteorHit");
                obj.transform.position = new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z);
                createCrater=true;
                SoundManager.Instance.PlaySFXSound("MeteorImpact");

                obj.transform.localScale = new Vector3(parentMissile.Range, parentMissile.Range, parentMissile.Range);
                for (int i = 0; i < 9; i++)
                {
                    obj.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).localScale = new Vector3(parentMissile.Range, parentMissile.Range, parentMissile.Range);
                }
                meteor.SetActive(false);
            }
        }
    }
}