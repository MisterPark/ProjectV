using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardOrb : MonoBehaviour
{
    float tick;
    float angle;
    public float cooldown;
    Missile parentMissile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        parentMissile = GetComponent<Missile>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        angle += 300 * Time.fixedDeltaTime;
        //if(angle>360)
        //{
        //    angle = 0;
        //}
        if(tick>cooldown)
        {
            tick = 0;

            GameObject obj = ObjectPool.Instance.Allocate("IceBolt");
            Missile missile = obj.GetComponent<Missile>();
            missile.Initialize();
            missile.transform.position = parentMissile.transform.position;
            missile.team = parentMissile.team;
            missile.owner = parentMissile.owner;
            missile.duration = parentMissile.duration;
            missile.damage = parentMissile.damage*0.6f;
            missile.range = parentMissile.range;
            missile.speed = parentMissile.speed*2;
            missile.type = MissileType.Directional;

            Quaternion v3Rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 v3Direction = parentMissile.targetDirection;
            Vector3 v3RotatedDirection = v3Rotation * v3Direction;
            missile.targetDirection = (v3RotatedDirection);
        }
        
    }
}
