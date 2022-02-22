using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum StatType
{
    Level,          // 레벨
    Might,          // 괴력 (Might): 랭크당 공격력 10% 증가
    Armor,          // 방어력 (Armor): 랭크당 피격 데미지 1 감소
    MaxHealth,      // 최대 체력 (Max Health): 랭크당 체력 20% 증가
    Health,         // 현재 체력
    Recovery,       // 회복 (Recovery): 랭크당 체력 회복 0.2 증가
    Cooldown,       // 쿨타임 (Cooldown): 랭크당 쿨타임 8% 감소
    Area,           // 공격범위 (Area): 랭크당 범위 10% 증가
    Speed,          // 투사체 속도 (Speed): 랭크당 투사체 속도 10% 증가
    Duration,       // 지속시간 (Duration): 랭크당 지속력 10% 증가
    Amount,         // 투사체 수 (Amount): 투사체 개수 +1
    MoveSpeed,      // 이동속도 (MoveSpeed): 랭크당 이동속도 10% 증가
    Magnet,         // 자석 (Magnet): 랭크당 획득반경 50% 증가
    Luck,           // 행운 (Luck): 랭크당 행운 수치 10% 증가
    Growth,         // 성장 (Growth): 랭크당 경험치 획득 8% 증가
    MaxExp,         // 최대 경험치
    Exp,            // 현재 경험치
    Greed,          // 탐욕 (Greed): 랭크당 골드 획득 10% 증가
    Gold,           // 소지 머니
    END
}


[System.Serializable]
public class Stats
{
    public StatType statType;
    public float origin_Stat;           // 캐릭터 스탯
    public float powerUp_Stat;          // 상점에서 강화하거나, 캐릭터 고유 패시브로 처음에 변경되는 origin_Stat의 스탯비율  // 누적
    public float growth_Stat;           // 레벨업시 or 상자 획득시 증가하는 origin_Stat의 스탯비율  // 고정
    public float final_Stat;            // 다 계산하고 난 뒤 스탯
}

public class Stat : MonoBehaviour
{
    [ArrayElementTitle("statType")]
    [SerializeField] protected Stats[] stats = new Stats[(int)StatType.END];

    // Start is called before the first frame update
    void Start()
    {
        Init_FinalStat();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init_FinalStat()
    {
        for (int i = 0; i < (int)StatType.END; i++)
        {
            stats[i].final_Stat = stats[i].origin_Stat + (stats[i].powerUp_Stat);
        }
        stats[(int)StatType.Health].final_Stat = stats[(int)StatType.MaxHealth].final_Stat;
    }

    public virtual void LevelUp()
    {
        stats[(int)StatType.Level].final_Stat += 1;
        stats[(int)StatType.Exp].final_Stat -= stats[(int)StatType.MaxExp].final_Stat;
        stats[(int)StatType.MaxExp].final_Stat += stats[(int)StatType.MaxExp].origin_Stat * stats[(int)StatType.MaxExp].growth_Stat;
    }

    /// <summary>
    /// 게임시작 이전 상점에서 아이템 구입시 능력치 증가
    /// </summary>
    public float Increase_PowerUpStat(StatType _statType)
    {
        switch (_statType)
        {
            case StatType.Armor:
                {
                    return stats[(int)StatType.Armor].powerUp_Stat += 1f;
                }
            case StatType.Recovery:
                {
                    return stats[(int)StatType.Recovery].powerUp_Stat += 1f;
                }
            case StatType.Amount:
                {
                    return stats[(int)StatType.Amount].powerUp_Stat += 1f;
                }
            default:
                {
                    return stats[(int)_statType].powerUp_Stat += stats[(int)_statType].powerUp_Stat * 0.5f;
                }
        }
    }

    /// <summary>
    /// 게임 시작 한후 보조장비 획득후 능력치 증가
    /// </summary>
    public float Increase_FinalStat(StatType _statType, int _count = 1)
    {
        switch (_statType)
        {
            case StatType.Armor:
                {
                    return stats[(int)StatType.Armor].final_Stat += _count;
                }
            case StatType.Recovery:
                {
                    return stats[(int)StatType.Recovery].final_Stat += _count;
                }
            case StatType.Amount:
                {
                    return stats[(int)StatType.Amount].final_Stat += _count;
                }
            default:
                {
                    return stats[(int)_statType].final_Stat += stats[(int)_statType].origin_Stat * (stats[(int)_statType].growth_Stat * _count);
                }
        }
    }

    public float Get_OriginStat(StatType _statType)
    {
        return stats[(int)_statType].final_Stat;
    }
    public float Get_PowerUpStat(StatType _statType)
    {
        return stats[(int)_statType].final_Stat;
    }
    public float Get_GrowthStat(StatType _statType)
    {
        return stats[(int)_statType].final_Stat;
    }
    public float Get_FinalStat(StatType _statType)
    {
        return stats[(int)_statType].final_Stat;
    }

    /// <summary>
    /// 기존 스탯에서 오른 총 비율 반환 ( 20% 은 0.2f 반환)
    /// </summary>
    public float Get_TotalPercentStat(StatType _statType)
    {
        switch (_statType)
        {
            case StatType.Armor:
                {
                    return stats[(int)StatType.Armor].final_Stat;
                }
            case StatType.Recovery:
                {
                    return stats[(int)StatType.Recovery].final_Stat;
                }
            case StatType.Amount:
                {
                    return stats[(int)StatType.Amount].final_Stat;
                }
            case StatType.Cooldown:
                {
                    return stats[(int)_statType].final_Stat - stats[(int)_statType].origin_Stat;
                }
            default:
                {
                    return (stats[(int)_statType].final_Stat / stats[(int)_statType].origin_Stat);
                }
        }
    }

    /// <summary>
    /// 체력회복용
    /// </summary>
    public void Set_FinalStat(StatType _statType, float _value)
    {
        stats[(int)_statType].final_Stat = _value;
    }

    /// <summary>
    /// 데미지를 입는
    /// </summary>
    public float Damaged(float _value)
    {
        _value -= stats[(int)StatType.Armor].final_Stat;
        if(_value > 0f)
            stats[(int)StatType.Health].final_Stat -= _value;
        return _value;
    }

    /// <summary>
    /// 상대에게 데미지를 주는
    /// </summary>
    public float TakeDamage(Stat otherStat)
    {
        return otherStat.Damaged(stats[(int)StatType.Might].final_Stat);
    }

}
