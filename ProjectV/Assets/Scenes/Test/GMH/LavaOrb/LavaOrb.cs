using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaOrb : MonoBehaviourEx, IFixedUpdater
{

    public float speed;
    public Transform target;
    
    protected override void Start()
    {
        base.Start();
        if (target == null)
        {
            target = Player.Instance.transform;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    
    public void FixedUpdateEx()
    {
            transform.RotateAround(target.transform.position, Vector3.up, speed * Time.fixedDeltaTime);
    }
}
