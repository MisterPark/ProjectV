using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviourEx, IFixedUpdater
{
    public float scale;
    public float speed = 10f;
    
    protected override void Start()
    {
        base.Start();
        transform.localScale = new Vector3(scale/5, scale / 5, scale / 5);

        Missile missile = GetComponent<Missile>();
        scale = missile.Range;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Start();
    }
    
    public void FixedUpdateEx()
    {
        if (transform.localScale.x<scale)
        { transform.localScale += new Vector3(0.3f, 0.3f, 0.3f) * speed * Time.fixedDeltaTime; }
    }
}
