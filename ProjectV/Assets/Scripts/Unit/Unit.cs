using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float moveSpeed = 2f;
    Vector3 oldPosition;
    public enum StatType
    {
        Level,          // ����
        Might,          // ���� (Might): ��ũ�� ���ݷ� 5% ����
        Armor,          // ���� (Armor): ��ũ�� �ǰ� ������ 1 ����
        MaxHealth,      // �ִ� ü�� (Max Health): ��ũ�� ü�� 10% ����
        Health,         // ���� ü��
        Recovery,       // ȸ�� (Recovery): ��ũ�� ü�� ȸ�� 0.1 ����
        Cooldown,       // ��Ÿ�� (Cooldown): ��ũ�� ��Ÿ�� 2.5% ����
        Area,           // ���ݹ��� (Area): ��ũ�� ���� 5% ����
        Speed,          // ����ü �ӵ� (Speed): ��ũ�� ����ü �ӵ� 10% ����
        Duration,       // ���ӽð� (Duration): ��ũ�� ���ӷ� 15% ����
        Amount,         // ����ü �� (Amount): ����ü ���� +1
        MoveSpeed,      // �̵��ӵ� (MoveSpeed): ��ũ�� �̵��ӵ� 5% ����
        Magnet,         // �ڼ� (Magnet): ��ũ�� ȹ��ݰ� 25% ����
        Luck,           // ��� (Luck): ��ũ�� ��� ��ġ 10% ����
        Growth,         // ���� (Growth): ��ũ�� ����ġ ȹ�� 3% ����
        MaxExp,         // �ִ� ����ġ
        Exp,            // ���� ����ġ
        Greed,          // Ž�� (Greed): ��ũ�� ��� ȹ�� 5% ����
        Gold,           // ���� �Ӵ�
        END
    }

    [SerializeField]
    public float[] origin_stats = new float[(int)StatType.END];
    public float[] growth_stats = new float[(int)StatType.END];
    public float[] final_stats = new float[(int)StatType.END];
    //

    protected Animator animator;

    public CapsuleCollider capsuleCollider;
    protected virtual void Start()
    {
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
        for (int i = 0; i < (int)StatType.END; i++)
        {
            final_stats[i] = origin_stats[i];
        }
    }

    protected virtual void Update()
    {
        bool isRun = oldPosition != transform.position;
        if(isRun)
        {
            oldPosition = transform.position;
        }
        animator.SetBool("IsRun", isRun);
        
    }


    public void MoveTo(Vector3 target)
    {
        Vector3 to = target - transform.position;
        Vector3 direction = to.normalized;
        
        transform.position += moveSpeed * direction * Time.deltaTime;
        transform.LookAt(target);
    }

    public virtual void stat_LevelUp()
    {
        final_stats[(int)StatType.Level] += 1;
        final_stats[(int)StatType.Exp] -= final_stats[(int)StatType.MaxExp];
        final_stats[(int)StatType.MaxExp] += growth_stats[(int)StatType.MaxExp];
    }
    public void stat_Increase(StatType _statType)
    {
        final_stats[(int)_statType] += growth_stats[(int)_statType];
    }
    public void stat_Increases(StatType _statType, int _Count)
    {
        final_stats[(int)_statType] += (growth_stats[(int)_statType] * _Count);
    }

    public float stat_GetFinal(StatType _statType)
    {
        return final_stats[(int)_statType];
    }

    public void stat_SetFinal(StatType _statType, float _value)
    {
        final_stats[(int)_statType] = _value;
    }

    
}
