using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMove : MonoBehaviourEx, IUpdater
{
    [SerializeField] float speed = 1;
    public void UpdateEx()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
