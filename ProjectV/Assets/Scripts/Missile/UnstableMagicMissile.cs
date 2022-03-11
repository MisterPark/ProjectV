using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableMagicMissile : Missile
{
    float movePatternTick = 1f;
    float movePatternCoolTime = 1f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void ProcessMove()
    {

        transform.position += targetDirection * speed * Time.fixedDeltaTime;
    }

    protected override void FixedUpdate()
    {
        movePatternTick += Time.fixedDeltaTime;
        if(movePatternTick > movePatternCoolTime)
        {
            movePatternTick = 0f;
            movePatternCoolTime = Random.Range(0.4f, 0.6f);
            targetDirection = new Vector3(Random.Range(-180f, 180f), 0f, Random.Range(-180f, 180f));
            targetDirection = targetDirection.normalized;

            Vector3 to = Player.Instance.transform.position - transform.position;
            float dist = to.magnitude;
            if(dist >= 7f)
            {
                to = to + targetDirection;
                float dist2 = to.magnitude;
                if (dist > dist2)
                {
                    targetDirection *= -1f;
                }
            }
        }
        base.FixedUpdate();
    }
}
