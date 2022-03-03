using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    //public iTween.EaseType movementEaseType = iTween.EaseType.easeInOutSine;
    public float movementRadius = 30;
    public float movementSpeed = 0.5f;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        //StartCoroutine(movementRadius());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
            StartCoroutine(IncreasePull(other, true));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
            StartCoroutine(IncreasePull(other, false));
    }

    IEnumerator Movement()
    {
        var newPos = new Vector3 (originalPosition.x+Random.Range(-movementRadius, movementRadius),originalPosition.y,originalPosition.z+Random.Range(-movementRadius,movementRadius));
        var distance = newPos - originalPosition;
        var time = distance.magnitude/movementSpeed;

        //iTween.MoveTo(gameObject, iTween.Hash("postion", newPos, "easeType", "time", time));
        yield return new WaitForSeconds(time+0.1f);
        StartCoroutine(Movement());
    }

    IEnumerator IncreasePull(Collider co, bool pull)
    {
        //if(pull)
        //{
        //    pullForce = pullForceCurve.Evaluate(((Time.time * 0.1f) % pullForceCurve.length));
        //    Vector3 forceDirection = pullingCenter.position - co.transform.position;
        //    co.GetComponent<Rigidbody>().AddForce(forceDirection.normalized*pullForce*Time.deltaTime);
        //    pullingCenter.position = new Vector3(pullingCenter.position.x,pullingCenterCurve.Evaluate((Time.time*0.1f)%pullingCenterCurve.length))
        //        yield return refreshRate;
        //    StartCoroutine(IncreasePull(co, pull));
        //}
        //else
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
