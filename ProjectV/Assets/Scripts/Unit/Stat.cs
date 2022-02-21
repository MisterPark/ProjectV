using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
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

    [SerializeField] public float[] origin_Stat = new float[(int)StatType.END];
    [SerializeField] public float[] growth_Stat = new float[(int)StatType.END];
    [SerializeField] public float[] final_Stat = new float[(int)StatType.END];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)Stat.StatType.END; i++)
        {
            final_Stat[i] = origin_Stat[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void LevelUp()
    {
        final_Stat[(int)StatType.Level] += 1;
        final_Stat[(int)StatType.Exp] -= final_Stat[(int)StatType.MaxExp];
        final_Stat[(int)StatType.MaxExp] += growth_Stat[(int)StatType.MaxExp];
    }
    public void StatIncrease(StatType _statType)
    {
        final_Stat[(int)_statType] += growth_Stat[(int)_statType];
    }
    public void StatIncreases(StatType _statType, int _Count)
    {
        final_Stat[(int)_statType] += (growth_Stat[(int)_statType] * _Count);
    }

    public float StatGetFinal(StatType _statType)
    {
        return final_Stat[(int)_statType];
    }

    public void StatSetFinal(StatType _statType, float _value)
    {
        final_Stat[(int)_statType] = _value;
    }
}
