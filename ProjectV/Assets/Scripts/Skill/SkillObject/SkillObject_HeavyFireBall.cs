using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_HeavyFireBall : SkillObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if (tick >= duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
            return;
        }

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Vector3 getPoint = Random.onUnitSphere;
        getPoint.y = 2f;
        getPoint = getPoint.normalized;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(getPoint * 800f, ForceMode.Impulse);
        rigidbody.velocity = Vector3.zero;


    }
}
