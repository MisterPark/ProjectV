using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerCharacterName
{
    Character_01,
    Character_02,
    Character_03,
    END
}
[System.Serializable]
public class PlayerCharacterNode
{
    [SerializeField] public PlayerCharacterName name;
    [SerializeField] public UnitStatData statsData;
}

public class Player : Unit
{
    public static Player Instance;
    private Vector3 direction = Vector3.forward;// ĳ���Ͱ� �ٶ󺸴� ����, ��ų ���� ��� 
    
    public static int Row
    {
        get 
        {
            int temp = (Instance.transform.position.z < 0f) ? -20 : 20;
            return ((int)Instance.transform.position.z + temp) / 40; 
        }
    }

    public static int Column
    {
        get 
        {
            int temp = (Instance.transform.position.x < 0f) ? -20 : 20;
            return ((int)Instance.transform.position.x + temp) / 40; 
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        Init_Stat();
        type = UnitType.Player;
        OnDead.AddListener(OnDeadCallback);
        OnLevelUp.AddListener(OnLevelUpCallback);

        //AddSkill(SkillKind.IceBolt);
        AddSkill(SkillKind.FireBolt);
        //AddSkill(SkillKind.ForceFieldBarrier);

    }

    protected override void Update()
    {
        base.Update();
        Move();
    }


    void Move()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector3 forward = transform.position - Camera.main.transform.position;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += right;
        }

        direction += moveDirection;
        // Move
        direction.Normalize();
        moveDirection.Normalize();
        transform.position += stat.Get_FinalStat(StatType.MoveSpeed) * moveDirection * Time.deltaTime;
        // Rotate
        transform.LookAt(transform.position + direction);

    }

    void OnDeadCallback()
    {
        // �÷��̾ �׾��� ��
    }

    void OnLevelUpCallback(int level)
    {
        // �÷��̾ ������ ���� ��

    }

    private void Init_Stat()
    {
        DataManager dataManager = DataManager.Instance;
        Stat _stat = GetComponent<Stat>();
        _stat.Set_Stats(dataManager.playerCharacterData[(int)dataManager.currentPlayerCharacter].statsData.stats);
        _stat.Init_FinalStat();
        for (int i = 0; i < (int)StatType.END; i++)
        {
            _stat.Set_PowerUpStat(i, dataManager.powerUpStat[i]);
        }
    }
}
