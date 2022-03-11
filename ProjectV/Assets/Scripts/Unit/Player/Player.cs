using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum PlayerCharacterName
{
    Character_01,
    Character_02,
    Character_03,
    Character_04,
    Character_05,
    END
}
[System.Serializable]
public class PlayerCharacterNode
{
    [SerializeField] public PlayerCharacterName name;
    [SerializeField] public UnitStatData statsData;
    [SerializeField] public Sprite charImage;
    [SerializeField] public SkillKind firstSkill;
    [SerializeField] public string description;
}

public class Player : Unit
{
    public static Player Instance;
    private Vector3 direction = Vector3.forward;// 주석 범인 찾기
    public UnityEvent OnSkillSelectionCompleted;
    
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

        // 캐릭터 기본스킬
        PlayerCharacterName charName = DataManager.Instance.currentGameData.characterName;
        //SkillKind skillKind = DataManager.Instance.playerCharacterData[(int)charName].firstSkill;
        //AddSkill(skillKind);

        //AddSkill(SkillKind.IceBolt);
        //AddSkill(SkillKind.FireBolt);
        //AddSkill(SkillKind.ForceFieldBarrier);
        //AddSkill(SkillKind.BlackHole);
        //AddSkill(SkillKind.Laser);
        //AddSkill(SkillKind.Lightning);

        //AddSkill(SkillKind.FireTornado);
        AddSkill(SkillKind.ToxicTotem);
        //AddSkill(SkillKind.ShurikenAttack);
        //AddSkill(SkillKind.Rain);
        //AddSkill(SkillKind.BlizzardOrb);
        OnSkillSelectionCompleted.Invoke();
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
        right.Normalize();

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += forward;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += -right;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += -forward;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += right;
        }
#endif
#if UNITY_EDITOR || UNITY_ANDROID
        float horizontal = GameManager.Instance.Joystick.Horizontal;
        float vertical = GameManager.Instance.Joystick.Vertical;
        Vector3 joystickInput = new Vector3(horizontal, 0, vertical);
        float angle = Vector3.SignedAngle(Vector3.forward, forward, Vector3.up);
        //Debug.Log($"{ joystickInput} {angle}");
        float radian = Mathf.Deg2Rad * -angle;
        float x = joystickInput.x * Mathf.Cos(radian) - joystickInput.z * Mathf.Sin(radian);
        float y = joystickInput.x * Mathf.Sin(radian) + joystickInput.z * Mathf.Cos(radian);
        moveDirection += new Vector3(x, 0, y);

#endif

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
    }

    void OnLevelUpCallback(int level)
    {
        GameManager.Instance.Pause();
        GameManager.Instance.ShowCursor();

        List<SkillInformation> skillInfos = MakeNewSkillInformations();

        UI_LevelUp.instance.SetSkillInfomations(skillInfos);
        UI_LevelUp.instance.Show();
    }

    void OnSelectSkill(SkillKind kind)
    {
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
        OnSkillSelectionCompleted.Invoke();
    }

    private void Init_Stat()
    {
        DataManager dataManager = DataManager.Instance;
        Stat _stat = GetComponent<Stat>();
        _stat.Set_Stats(dataManager.playerCharacterData[(int)dataManager.currentGameData.characterName].statsData.stats);
        _stat.Init_LoadStat();
        for (int i = 0; i < (int)StatType.END; i++)
        {
            _stat.Set_PowerUpStat(i, dataManager.powerUpStat[i]);
        }
        _stat.Init_FinalStat();
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

        List<SkillKind> kinds = new List<SkillKind>();

        for (int index = (int)SkillKind.IceBolt; index < (int)SkillKind.End; index++)
        {
            SkillKind kind = (SkillKind)index;
            SkillType type = kind.GetSkillType();
            Skill skill = FindSkill(kind);

            if (type == SkillType.Active && skill == null && actives.Count >= 6) continue;
            if (type == SkillType.Passive && skill == null && passives.Count >= 6) continue;
            if (skill != null && skill.IsMaxLevel) continue;

            kinds.Add(kind);
        }


        if (kinds.Count == 0)
        {

        }
        else
        {
            int maxCount = 3;
            int count = 0;

            while (count < maxCount)
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
        }

        




        return skillInfos;
    }


}
