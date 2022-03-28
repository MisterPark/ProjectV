using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float scale;
    public float speed = 10f;
    
    void Start()
    {
        transform.localScale = new Vector3(scale/5, scale / 5, scale / 5);

        Missile missile = GetComponent<Missile>();
        scale = missile.Range;
    }

    private void OnEnable()
    {
        Start();
    }
    
    void FixedUpdate()
    {
        if (transform.localScale.x<scale)
        { transform.localScale += new Vector3(0.3f, 0.3f, 0.3f) * speed * Time.fixedDeltaTime; }
    }
}
