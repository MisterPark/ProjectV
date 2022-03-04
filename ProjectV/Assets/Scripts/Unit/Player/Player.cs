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
    private Vector3 direction = Vector3.forward;// 캐릭터가 바라보는 방향, 스킬 사용시 사용 
    
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
        UI_LevelUp.instance.OnSelected.AddListener(OnSelectSkill);

        AddSkill(SkillKind.IceBolt);
        //AddSkill(SkillKind.FireBolt);
        //AddSkill(SkillKind.ForceFieldBarrier);
        //AddSkill(SkillKind.BlackHole);
        //AddSkill(SkillKind.Laser);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
        transform.position += stat.Get_FinalStat(StatType.MoveSpeed) * moveDirection * Time.fixedDeltaTime;
        // Rotate
        transform.LookAt(transform.position + direction);

    }

    void OnDeadCallback()
    {
        // 플레이어가 죽었을 때
    }

    void OnLevelUpCallback(int level)
    {
        // 플레이어가 레벨업 했을 떄
        GameManager.Instance.Pause();
        GameManager.Instance.ShowCursor();

        List<SkillInformation> skillInfos = MakeNewSkillInformations();

        UI_LevelUp.instance.SetSkillInfomations(skillInfos);
        UI_LevelUp.instance.Show();
    }

    void OnSelectSkill(SkillKind kind)
    {
        // 레벨업 후 스킬 선택했을 때
        GameManager.Instance.Resume();
        GameManager.Instance.HideCursor();

        Skill skill = FindSkill(kind);
        if(skill != null)
        {
            skill.LevelUp();
        }
        else
        {
            AddSkill(kind);
        }

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

    List<Skill> GetActiveSkills()
    {
        List<Skill> resuls = new List<Skill>();
        foreach(Skill skill in Skills)
        {
            SkillData data = skill.SkillData;
            if(data.type == SkillType.Active)
            {
                resuls.Add(skill);
            }
        }

        return resuls;
    }

    List<Skill> GetPassiveSkills()
    {
        List<Skill> resuls = new List<Skill>();
        foreach (Skill skill in Skills)
        {
            SkillData data = skill.SkillData;
            if (data.type == SkillType.Passive)
            {
                resuls.Add(skill);
            }
        }

        return resuls;
    }

    Skill FindSkill(SkillKind kind)
    {
        int count = Skills.Count;
        for (int i = 0; i < count; i++)
        {
            Skill skill = Skills[i];
            if(skill.Kind == kind)
            {
                return skill;
            }
        }

        return null;
    }

    List<SkillInformation> MakeNewSkillInformations()
    {
        List<SkillInformation> skillInfos = new List<SkillInformation>();

        List<Skill> actives = GetActiveSkills();
        List<Skill> passives = GetPassiveSkills();

        // 추가 가능한 스킬의 종류
        List<SkillKind> kinds = new List<SkillKind>();

        // 추가 가능한지 거르는 로직
        for (int index = (int)SkillKind.IceBolt; index < (int)SkillKind.End; index++)
        {
            SkillKind kind = (SkillKind)index;
            SkillType type = kind.GetSkillType();
            Skill skill = FindSkill(kind);
            // 여기서 걸러야 함

            // 액티브일때 액티브가 꽉찼는가?
            if (type == SkillType.Active && actives.Count >= 6) continue;
            // 패시브일때 패시브가 꽉찼는가?
            if (type == SkillType.Passive && passives.Count >= 6) continue;
            // 만랩인가?
            if (skill != null && skill.IsMaxLevel) continue;

            kinds.Add(kind);
        }

        // TODO : 만약 모두 걸러졌으면 체력 or 치킨
        if(kinds.Count == 0)
        {
            
        }

        // TODO : 행운으로 4개까지 가능하게 바꿔야함
        int maxCount = 3;
        int count = 0;

        while(count < maxCount)
        {
            if (kinds.Count == 0) break;
            int random = Random.Range(0, kinds.Count);
            SkillKind kind = kinds[random];
            Skill skill = FindSkill(kind);
            int nextLevel = 1;
            if (skill != null)
            {
                nextLevel = skill.level + 1;
            }
            SkillInformation info = new SkillInformation(kind, nextLevel);
            skillInfos.Add(info);
            kinds.RemoveAt(random);
            count++;
        }


        return skillInfos;
    }


}
