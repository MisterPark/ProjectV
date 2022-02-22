using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public UnityEvent OnDead;
    [HideInInspector] public Stat stat;
    public float moveSpeed;
    Vector3 oldPosition;

    protected Animator animator;

    public CapsuleCollider capsuleCollider;
    protected virtual void Start()
    {
        stat = GetComponent<Stat>();
        animator = GetComponentInChildren<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator Not Found");
        }
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        if(capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider Not Found");
        }
        oldPosition = transform.position;
    }

    protected virtual void Update()
    {
        Animation();

    }


    public void MoveTo(Vector3 target)
    {
        Vector3 to = target - transform.position;
        Vector3 direction = to.normalized;
        
        transform.position += stat.Get_FinalStat(StatType.MoveSpeed) * direction * Time.deltaTime;
        transform.LookAt(target);
    }




    void Animation()
    {
        bool isRun = oldPosition != transform.position;
        if (isRun)
        {
            oldPosition = transform.position;
        }
        animator.SetBool("IsRun", isRun);

    }

    

    
}
