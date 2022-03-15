using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaOrb : MonoBehaviour
{

    public float speed;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = Player.Instance.transform; 
        }
    }

    private void OnEnable()
    {
        Start();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
            transform.RotateAround(target.transform.position, Vector3.up, speed * Time.fixedDeltaTime);
    }
}
