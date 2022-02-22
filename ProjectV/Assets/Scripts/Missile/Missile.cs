using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissileType
{
    Directional,
    Guided,
}

public class Missile : MonoBehaviour
{
    [SerializeField]public MissileType type;
    [SerializeField]public float duration;
    [SerializeField]public float speed;
    [SerializeField]public float damage;

    GameObject target;
    Vector3 targetDirection;
    float tick = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        tick += Time.deltaTime;
        if(tick >= duration)
        {
            tick = 0f;
            ObjectPool.Instance.Free(gameObject);
            return;
        }

        ProcessMove();
        ProcessRotate();
    }

    void ProcessMove()
    {
        if(type == MissileType.Guided)
        {
            if(target != null)
            {
                Vector3 to = target.transform.position - transform.position;
                targetDirection = to.normalized;
            }
        }

        transform.position += targetDirection * speed * Time.deltaTime;
    }

    void ProcessRotate()
    {
        Vector3 targetPos = Vector3.zero;
        switch (type)
        {
            case MissileType.Directional:
                targetPos = transform.position + targetDirection;
                break;
            case MissileType.Guided:
                {
                    if(target != null)
                    {
                        targetPos = target.transform.position;
                    }
                }
                break;
            default:
                targetPos = transform.position + targetDirection;
                break;
        }

        transform.LookAt(targetPos);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetTarget(Vector3 target)
    {
        Vector3 to = target - transform.position;
        targetDirection = to.normalized;
    }
}
