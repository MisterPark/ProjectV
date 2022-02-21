using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { HpPotion, ExpSmall, ExpNormal,ExpBig };

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
            return;
    }
}
