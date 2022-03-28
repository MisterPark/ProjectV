using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaOrb : MonoBehaviour
{

    public float speed;
    public Transform target;
    
    void Start()
    {
        if (target == null)
        {
            target = Player.Instance.transform;
        }
    }

    private void OnEnable()
    {
        
    }
    
    void FixedUpdate()
    {
            transform.RotateAround(target.transform.position, Vector3.up, speed * Time.fixedDeltaTime);
    }
}
