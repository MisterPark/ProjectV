using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMove : MonoBehaviourEx
{
    [SerializeField] float speed = 1;
    public override void UpdateEx()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
