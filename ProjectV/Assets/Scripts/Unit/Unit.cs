using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    // ÀÌº¥Æ®
    public UnityEvent OnDead;
    public UnityEvent<float> OnTakeDamage;
    // ½ºÅÈ
    [HideInInspector] public Stat stat;

    // ÄÄÆ÷³ÍÆ®
    protected Animator animator;
    public CapsuleCollider capsuleCollider;
    
    // ³»ºÎ
    Vector3 oldPosition;
    public Vector3 skillOffsetPosition;
    public Team team;

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
        OnTakeDamage.AddListener(OnTakeDamageCallback);
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

    void OnTakeDamageCallback(float damage)
    {

        GameObject temp = ObjectPool.Instance.Allocate("UI_DamageFont");
        UI_DamageFont font = temp.transform.GetChild(0).GetComponent<UI_DamageFont>();
        font.Init((int)damage, UI_DamageFont.FontColor.WHITE, transform.position + (Vector3.up * 2f));

        // »ç¸ÁÃ³¸®
        float hp = stat.Get_FinalStat(StatType.Health);
        if (hp <= 0f)
        {
            OnDead?.Invoke();
        }

    }

    
}
