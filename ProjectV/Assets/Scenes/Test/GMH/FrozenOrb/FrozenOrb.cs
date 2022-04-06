using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenOrb : MonoBehaviourEx
{
    float tick;
    float angle;
    public float cooldown;
    Missile parentMissile;
    
    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        if (parentMissile == null)
        {
            parentMissile = GetComponent<Missile>();
        }
        cooldown = parentMissile.Delay;
    }
    
    public override void FixedUpdateEx()
    {
        if(cooldown<=0)
        {
            OnEnable();
            return;
        }

        tick += Time.fixedDeltaTime;
        angle += 300 * Time.fixedDeltaTime;

        if(tick>cooldown)
        {
            tick = 0;
            GameObject obj = ObjectPool.Instance.Allocate("IceBolt");
            Missile missile = obj.GetComponent<Missile>();
            missile.Initialize();
            missile.transform.position = parentMissile.transform.position;
            missile.Team = parentMissile.Team;
            missile.Owner = parentMissile.Owner;
            missile.Duration = parentMissile.Duration;
            missile.Damage = parentMissile.Damage*0.75f;
            missile.Range = parentMissile.Range;
            missile.Speed = parentMissile.Speed*2;
            missile.Type = MissileType.Directional;
            missile.IsPull = true;
            Quaternion v3Rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 v3Direction = parentMissile.TargetDirection;
            Vector3 v3RotatedDirection = v3Rotation * v3Direction;
            missile.TargetDirection = (v3RotatedDirection);
            SoundManager.Instance.PlaySFXSound("IceBolt");
        }
    }
}
