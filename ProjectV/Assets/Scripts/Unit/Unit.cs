using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UnitType
{
    None,
    Player,
    Monster,
}


public class Unit : MonoBehaviour
{
    public UnitType type;
    // 이벤트
    public UnityEvent OnDead;
    public UnityEvent<float> OnTakeDamage;
    // 스탯
    [HideInInspector] public Stat stat;

    // 컴포넌트
    protected Animator animator;
    public CapsuleCollider capsuleCollider;
    
    // 내부
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

    public void AddSkill(SkillType type)
    {
        if (type == SkillType.None) return;

        switch (type)
        {
            case SkillType.IceBolt: gameObject.AddComponent<Skill_IceBolt>(); break;
            case SkillType.FireBolt: gameObject.AddComponent<Skill_FireBolt>(); break;
            default:
                break;
        }
    }




    void Animation()
    {
        // 유닛 타입 세팅
        animator.SetInteger("UnitType", (int)type);
        // 달리기
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

        // 사망처리
        float hp = stat.Get_FinalStat(StatType.Health);
        if (hp <= 0f)
        {
            OnDead?.Invoke();
        }

    }

    
}
