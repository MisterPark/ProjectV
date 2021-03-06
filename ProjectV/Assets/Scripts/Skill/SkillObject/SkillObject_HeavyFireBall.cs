using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_HeavyFireBall : SkillObject
{
    
    public override void FixedUpdateEx()
    {
        base.FixedUpdateEx();
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
        rigidbody.AddForce(getPoint * 1000f, ForceMode.Impulse);
        rigidbody.velocity = Vector3.zero;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(tick < 1f)
        {
            return;
        }
        if (other.gameObject.tag == "Board")
        {
            //GameObject obj = ObjectPool.Instance.Allocate("MeteorHit");
            //obj.transform.position = new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z);
            tick = duration;

            GameObject obj = ObjectPool.Instance.Allocate("HeavyFireBallImpact");
            Missile missile = Missile.Find(obj);
            missile.Initialize();
            missile.transform.position = transform.position;
            missile.Team = team;
            missile.Owner = owner;
            missile.Duration = 0.4f;
            missile.Damage = damage;
            missile.Range = range;
            missile.Speed = speed;
            missile.Delay = delay;
            missile.IsPenetrate = true;
            missile.Type = MissileType.Other;
            missile.transform.GetChild(0).gameObject.SetActive(true);
            var shape = missile.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            shape.radius = range * 0.5f;
            //missile.OnCollision.RemoveAllListeners();
            //missile.OnCollision.AddListener(OnCollisionCallback);
        }
    }
}
