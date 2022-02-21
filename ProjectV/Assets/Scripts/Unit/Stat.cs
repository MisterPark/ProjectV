using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
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
