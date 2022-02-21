using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum StatType
    {
        Level,          // 레벨
        Might,          // 괴력 (Might): 랭크당 공격력 5% 증가
        Armor,          // 방어력 (Armor): 랭크당 피격 데미지 1 감소
        MaxHealth,      // 최대 체력 (Max Health): 랭크당 체력 10% 증가
        Health,         // 현재 체력
        Recovery,       // 회복 (Recovery): 랭크당 체력 회복 0.1 증가
        Cooldown,       // 쿨타임 (Cooldown): 랭크당 쿨타임 2.5% 감소
        Area,           // 공격범위 (Area): 랭크당 범위 5% 증가
        Speed,          // 투사체 속도 (Speed): 랭크당 투사체 속도 10% 증가
        Duration,       // 지속시간 (Duration): 랭크당 지속력 15% 증가
        Amount,         // 투사체 수 (Amount): 투사체 개수 +1
        MoveSpeed,      // 이동속도 (MoveSpeed): 랭크당 이동속도 5% 증가
        Magnet,         // 자석 (Magnet): 랭크당 획득반경 25% 증가
        Luck,           // 행운 (Luck): 랭크당 행운 수치 10% 증가
        Growth,         // 성장 (Growth): 랭크당 경험치 획득 3% 증가
        MaxExp,         // 최대 경험치
        Exp,            // 현재 경험치
        Greed,          // 탐욕 (Greed): 랭크당 골드 획득 5% 증가
        Gold,           // 소지 머니
        END
    }

    [SerializeField]
    public float[] origin_stats = new float[(int)StatType.END];
    public float[] growth_stats = new float[(int)StatType.END];
    public float[] final_stats = new float[(int)StatType.END];
    //

    protected Animator animator;
    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator Not Found");
        }
        for (int i = 0; i < (int)StatType.END; i++)
        {
            final_stats[i] = origin_stats[i];
        }
    }

    protected virtual void Update()
    {
        
    }

    public void MoveTo(Vector3 position)
    {

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
