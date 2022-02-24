using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum StatType
{
    Level,          // ����
    Might,          // ���� (Might): ��ũ�� ���ݷ� 10% ����
    Armor,          // ���� (Armor): ��ũ�� �ǰ� ������ 1 ����
    MaxHealth,      // �ִ� ü�� (Max Health): ��ũ�� ü�� 20% ����
    Health,         // ���� ü��
    Recovery,       // ȸ�� (Recovery): ��ũ�� ü�� ȸ�� 0.2 ����
    Cooldown,       // ��Ÿ�� (Cooldown): ��ũ�� ��Ÿ�� 8% ����
    Area,           // ���ݹ��� (Area): ��ũ�� ���� 10% ����
    Speed,          // ����ü �ӵ� (Speed): ��ũ�� ����ü �ӵ� 10% ����
    Duration,       // ���ӽð� (Duration): ��ũ�� ���ӷ� 10% ����
    Amount,         // ����ü �� (Amount): ����ü ���� +1
    MoveSpeed,      // �̵��ӵ� (MoveSpeed): ��ũ�� �̵��ӵ� 10% ����
    Magnet,         // �ڼ� (Magnet): ��ũ�� ȹ��ݰ� 50% ����
    Luck,           // ��� (Luck): ��ũ�� ��� ��ġ 10% ����
    Growth,         // ���� (Growth): ��ũ�� ����ġ ȹ�� 8% ����
    MaxExp,         // �ִ� ����ġ
    Exp,            // ���� ����ġ
    Greed,          // Ž�� (Greed): ��ũ�� ��� ȹ�� 10% ����
    Gold,           // ���� �Ӵ�
    END
}


[System.Serializable]
public class Stats
{
    public StatType statType;
    public float origin_Stat;           // ĳ���� ����
    public float powerUp_Stat;          // �������� ��ȭ�ϰų�, ĳ���� ���� �нú�� ó���� ����Ǵ� origin_Stat�� ���Ⱥ���  // ����
    public float growth_Stat;           // �������� or ���� ȹ��� �����ϴ� origin_Stat�� ���Ⱥ���  // ����
    public float final_Stat;            // �� ����ϰ� �� �� ����
}

public class Stat : MonoBehaviour
{
    [SerializeField] private UnitStatData statsData;
    [ArrayElementTitle("statType")]
    [SerializeField] protected Stats[] stats;
    Unit owner;
    // Start is called before the first frame update
    void Start()
    {
        if (statsData != null)
        {
            stats = statsData.stats;
        }
        owner = GetComponent<Unit>();
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
            stats[i].final_Stat = stats[i].origin_Stat + (stats[i].origin_Stat * stats[i].powerUp_Stat);

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
        stats[(int)StatType.Exp].final_Stat -= stats[(int)StatType.MaxExp].final_Stat;
        stats[(int)StatType.MaxExp].final_Stat += stats[(int)StatType.MaxExp].origin_Stat * stats[(int)StatType.MaxExp].growth_Stat;
    }

    /// <summary>
    /// ���ӽ��� ���� �������� ������ ���Խ� �ɷ�ġ ����
    /// </summary>
    //public float Increase_PowerUpStat(StatType _statType)
    //{
    //    switch (_statType)
    //    {
    //        case StatType.Armor:
    //            {
    //                return stats[(int)StatType.Armor].powerUp_Stat += 1f;
    //            }
    //        case StatType.Recovery:
    //            {
    //                return stats[(int)StatType.Recovery].powerUp_Stat += 1f;
    //            }
    //        case StatType.Amount:
    //            {
    //                return stats[(int)StatType.Amount].powerUp_Stat += 1f;
    //            }
    //        default:
    //            {
    //                return stats[(int)_statType].powerUp_Stat += stats[(int)_statType].powerUp_Stat * 0.5f;
    //            }
    //    }
    //}

    /// <summary>
    /// ���� ���� ���� ������� ȹ���� �ɷ�ġ ����
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
            case StatType.Gold:
                {
                    return stats[(int)StatType.Gold].final_Stat += _count;
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
                    stats[(int)StatType.Exp].final_Stat += _count;
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
    /// ���� ���ȿ��� ���� �� ���� ��ȯ ( 20% �� 0.2f ��ȯ)
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
    /// �÷��̾� ������ �ε��
    /// </summary>
    public void Set_Stats(Stats[] _stat)
    {
        stats = _stat;
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
    /// ü��ȸ����
    /// </summary>
    public void Set_FinalStat(StatType _statType, float _value)
    {
        stats[(int)_statType].final_Stat = _value;
    }


    /// <summary>
    /// �������� �Դ�
    /// </summary>
    public float TakeDamage(float _value)
    {
        _value -= stats[(int)StatType.Armor].final_Stat;
        if(_value > 0f)
        {
            stats[(int)StatType.Health].final_Stat -= _value;
            owner.OnTakeDamage?.Invoke(_value);
        }

        return _value;
    }

    /// <summary>
    /// ��뿡�� �������� �ִ�
    /// </summary>
    public float ApplyDamage(Stat otherStat, float _value)
    {
        return otherStat.TakeDamage(_value);
    }


}
