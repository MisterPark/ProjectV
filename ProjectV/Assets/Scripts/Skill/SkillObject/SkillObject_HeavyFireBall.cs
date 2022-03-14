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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Board")
        {
            //GameObject obj = ObjectPool.Instance.Allocate("MeteorHit");
            //obj.transform.position = new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z);
            tick = duration;

            GameObject obj = ObjectPool.Instance.Allocate("HeavyFireBallImpact");
            Missile missile = obj.GetComponent<Missile>();
            missile.Initialize();
            missile.transform.position = transform.position;
            missile.team = team;
            missile.owner = owner;
            missile.duration = 0.3f;
            missile.damage = damage;
            missile.range = range;
            missile.speed = speed;
            missile.delay = delay;
            missile.isPenetrate = true;
            missile.type = MissileType.Other;

            //missile.OnCollision.RemoveAllListeners();
            //missile.OnCollision.AddListener(OnCollisionCallback);
        }
    }
}
