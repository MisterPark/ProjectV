using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public enum StatType
{
    Level,          // 레벨
    Strength,          // 괴력 (Strength): 랭크당 공격력 10% 증가
    Armor,          // 방어력 (Armor): 랭크당 피격 데미지 1 감소
    MaxHealth,      // 최대 체력 (Max Health): 랭크당 최대 체력 20% 증가
    Health,         // 현재 체력
    Recovery,       // 회복 (Recovery): 랭크당 체력 회복 0.2 증가
    Cooldown,       // 쿨타임 (Cooldown): 랭크당 쿨타임 8% 감소
    Range,           // 공격범위 (Range): 랭크당 범위 10% 증가
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
    [SerializeField] private UnitStatData statsData;
#if UNITY_EDITOR
    [ArrayElementTitle("statType")]
#endif
    [SerializeField] protected Stats[] stats;

    public UnityEvent<int> OnLevelUp;
    public UnityEvent<float> OnTakeDamage;

    void FixedUpdate()
    {
        // 체젠
        if(stats[(int)StatType.Recovery].final_Stat != 0f)
        {
            stats[(int)StatType.Health].final_Stat += stats[(int)StatType.Recovery].final_Stat * Time.fixedDeltaTime;
            if(stats[(int)StatType.Health].final_Stat > stats[(int)StatType.MaxHealth].final_Stat)
            {
                stats[(int)StatType.Health].final_Stat = stats[(int)StatType.MaxHealth].final_Stat;
            }
        }
    }

    public void Init_LoadStat()
    {
        if (statsData != null)
        {
            // 깊은복사
            stats = new Stats[(int)StatType.END];
            for (int i = 0; i < (int)StatType.END; i++)
            {
                stats[i] = new Stats();
                stats[i].statType = statsData.stats[i].statType;
                stats[i].origin_Stat = statsData.stats[i].origin_Stat;
                //stats[i].powerUp_Stat = statsData.stats[i].powerUp_Stat;
                stats[i].growth_Stat = statsData.stats[i].growth_Stat;
                stats[i].final_Stat = statsData.stats[i].final_Stat;
            }
        }
    }
      /////////////////////////////////////////////////////////////////////////
    public void Init_FinalStat()
    { 
            for (int i = 0; i < (int)StatType.END; i++)
        {
            switch ((StatType)i)
            {
                case StatType.Armor:
                    {
                        stats[(int)StatType.Armor].final_Stat = stats[i].origin_Stat + stats[i].powerUp_Stat;
                        break;
                    }
                case StatType.Recovery:
                    {
                        stats[(int)StatType.Recovery].final_Stat = stats[i].origin_Stat + stats[i].powerUp_Stat;
                        break;
                    }
                case StatType.Amount:
                    {
                        stats[(int)StatType.Amount].final_Stat = stats[i].origin_Stat + stats[i].powerUp_Stat;
                        break;
                    }
                default:
                    {
                        stats[i].final_Stat = stats[i].origin_Stat + (stats[i].origin_Stat * stats[i].powerUp_Stat);
                        break;
                    }
            }
        }
        stats[(int)StatType.Health].final_Stat = stats[(int)StatType.MaxHealth].final_Stat;
    }

    public virtual void LevelUp()
    {
        stats[(int)StatType.Level].final_Stat += 1;
        DataManager.Instance.currentGameData.playerLevel += 1;
        stats[(int)StatType.Exp].final_Stat -= stats[(int)StatType.MaxExp].final_Stat;
        //stats[(int)StatType.MaxExp].final_Stat += stats[(int)StatType.MaxExp].origin_Stat * stats[(int)StatType.MaxExp].growth_Stat;
        stats[(int)StatType.MaxExp].final_Stat *= stats[(int)StatType.MaxExp].growth_Stat + 1f;
        OnLevelUp?.Invoke(Mathf.RoundToInt(stats[(int)StatType.Level].final_Stat));
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
            case StatType.Health:
                {
                    stats[(int)StatType.Health].final_Stat += _count;
                    if (stats[(int)StatType.Health].final_Stat > stats[(int)StatType.MaxHealth].final_Stat)
                    {
                        stats[(int)StatType.Health].final_Stat = stats[(int)StatType.MaxHealth].final_Stat;
                    }
                    return stats[(int)StatType.Health].final_Stat;
                }
            case StatType.Exp:
                {
                    stats[(int)StatType.Exp].final_Stat += _count * stats[(int)StatType.Growth].final_Stat;
                    if (stats[(int)StatType.Exp].final_Stat > stats[(int)StatType.MaxExp].final_Stat)
                    {
                        LevelUp();
                    }
                    return stats[(int)StatType.Exp].final_Stat;
                }
            default:
                {
                    return stats[(int)_statType].final_Stat += stats[(int)_statType].origin_Stat * (stats[(int)_statType].growth_Stat * _count);
                }
        }
    }

    public float Increase_FinalStat(StatType _statType, float _count)
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
            case StatType.Health:
                {
                    stats[(int)StatType.Health].final_Stat += _count;
                    if (stats[(int)StatType.Health].final_Stat > stats[(int)StatType.MaxHealth].final_Stat)
                    {
                        stats[(int)StatType.Health].final_Stat = stats[(int)StatType.MaxHealth].final_Stat;
                    }
                    return stats[(int)StatType.Health].final_Stat;
                }
            case StatType.Exp:
                {
                    stats[(int)StatType.Exp].final_Stat += _count * stats[(int)StatType.Growth].final_Stat;
                    if (stats[(int)StatType.Exp].final_Stat > stats[(int)StatType.MaxExp].final_Stat)
                    {
                        LevelUp();
                    }
                    return stats[(int)StatType.Exp].final_Stat;
                }
            default:
                {
                    return stats[(int)_statType].final_Stat += stats[(int)_statType].origin_Stat * (stats[(int)_statType].growth_Stat * _count);
                }
        }
    }


    public float Get_OriginStat(StatType _statType)
    {
        return stats[(int)_statType].origin_Stat;
    }
    public float Get_PowerUpStat(StatType _statType)
    {
        return stats[(int)_statType].powerUp_Stat;
    }
    public float Get_GrowthStat(StatType _statType)
    {
        return stats[(int)_statType].growth_Stat;
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
            case StatType.MaxHealth:
                {
                    return stats[(int)StatType.MaxHealth].final_Stat;
                }
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
            //case StatType.Cooldown:
            //    {
            //        return stats[(int)_statType].final_Stat - stats[(int)_statType].origin_Stat;
            //    }
            default:
                {
                    return (stats[(int)_statType].final_Stat / stats[(int)_statType].origin_Stat);
                }
        }
    }

    /// <summary>
    /// 플레이어 데이터 로드용
    /// </summary>
    public void Set_Stats(Stats[] _stat)
    {
        //stats = _stat;
        statsData = DataManager.Instance.playerCharacterData[(int)DataManager.Instance.currentGameData.characterName].statsData;
    }
    public void Set_PowerUpStat(StatType _statType, float _value)
    {
        stats[(int)_statType].powerUp_Stat = _value;
    }
    public void Set_PowerUpStat(int _statType, float _value)
    {
        stats[_statType].powerUp_Stat = _value;
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
    public float TakeDamage(float _value)
    {
        _value -= stats[(int)StatType.Armor].final_Stat;
        if(_value > 0f)
        {
            stats[(int)StatType.Health].final_Stat -= _value;
            OnTakeDamage?.Invoke(_value);
        }

        return _value;
    }

    /// <summary>
    /// 상대에게 데미지를 주는
    /// </summary>
    public float ApplyDamage(Stat otherStat, float _value)
    {
        return otherStat.TakeDamage(_value);
    }

    public void RecoverToFull()
    {
        stats[(int)StatType.Health].final_Stat = stats[(int)StatType.MaxHealth].final_Stat;
    }
}
