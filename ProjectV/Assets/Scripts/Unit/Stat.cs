using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Level,          // ����
    Might,          // ���� (Might): ��ũ�� ���ݷ� 5% ����
    Armor,          // ���� (Armor): ��ũ�� �ǰ� ������ 1 ����
    MaxHealth,      // �ִ� ü�� (Max Health): ��ũ�� ü�� 20% ����
    Health,         // ���� ü��
    Recovery,       // ȸ�� (Recovery): ��ũ�� ü�� ȸ�� 0.2 ����
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

[System.Serializable]
public class Stats
{
    public StatType statType;
    public float origin_Stat;
    public float growth_Stat;
    public float final_Stat;
}

public class Stat : MonoBehaviour
{
    [ArrayElementTitle("statType")]
    [SerializeField] public Stats[] stats = new Stats[(int)StatType.END];

    //[SerializeField] public float[] origin_Stat = new float[(int)StatType.END];
    //[SerializeField] public float[] growth_Stat = new float[(int)StatType.END];
    //[SerializeField] public float[] final_Stat = new float[(int)StatType.END];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)StatType.END; i++)
        {
            stats[i].final_Stat = stats[i].origin_Stat;
        }
        stats[(int)StatType.Health].final_Stat = stats[(int)StatType.MaxHealth].final_Stat;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void LevelUp()
    {
        stats[(int)StatType.Level].final_Stat += 1;
        stats[(int)StatType.Exp].final_Stat -= stats[(int)StatType.MaxExp].final_Stat;
        stats[(int)StatType.MaxExp].final_Stat += stats[(int)StatType.MaxExp].growth_Stat;
    }
    public void StatIncrease(StatType _statType)
    {
        stats[(int)_statType].final_Stat += stats[(int)_statType].growth_Stat;
    }
    public void StatIncreases(StatType _statType, int _Count)
    {
        stats[(int)_statType].final_Stat += (stats[(int)_statType].growth_Stat * _Count);
    }

    public float StatGetFinal(StatType _statType)
    {
        return stats[(int)_statType].final_Stat;
    }

    public void StatSetFinal(StatType _statType, float _value)
    {
        stats[(int)_statType].final_Stat = _value;
    }

    // �������� �Դ�
    public float Damaged(float _value)
    {
        _value -= stats[(int)StatType.Armor].final_Stat;
        if(_value > 0f)
            stats[(int)StatType.Health].final_Stat -= _value;
        return _value;
    }

    // ��뿡�� �������� �ִ�
    public float TakeDamage(Stat otherStat)
    {
        return otherStat.Damaged(stats[(int)StatType.Might].final_Stat);
    }
}
