using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveTutScript : MonoBehaviourEx, IFixedUpdater
{

    public float speed;
    public GameObject impactPrefab;
    public List<GameObject> trails;

    private Rigidbody rb;
    
    protected override void Start()
    {
        base.Start();
        rb =GetComponent<Rigidbody>();
    }

    
    public void FixedUpdateEx()
    {
        
        if(speed!=0&&rb!=null)
        {
            rb.position+=transform.forward*(speed*Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up,contact.normal);
        Vector3 pos = contact.point;

        if(impactPrefab!=null)
        {
            var impactVFX = ObjectPool.Instance.Allocate("MeteorHit", pos, rot) as GameObject;
            Destroy(impactVFX, 5);

        }

        if(trails.Count >0)
        {
            for(int i = 0; i<trails.Count; i++)
            {
                trails[i].transform.parent = null;
                var ps = trails[i].GetComponent<ParticleSystem>();
                if(ps!=null)
                {
                    ps.Stop();
                    Destroy(ps.gameObject,ps.main.duration+ps.main.startLifetime.constantMax);
                }
            }
        }

        Destroy(gameObject);
    }
}
