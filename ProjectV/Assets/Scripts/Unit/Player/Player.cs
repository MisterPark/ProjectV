using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;





public class Player : Unit
{
    public static Player Instance;
    private Vector3 direction = Vector3.forward;// 주석 범인 찾기
    private float damageTick = 0f;
    private float damageDelay = 0.2f;
    private bool damageFlag = false;

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

        
        PlayerCharacterName charName = DataManager.Instance.currentGameData.characterName;
        PlayerCharacter data = DataManager.Instance.playerCharacterData[(int)charName].playerCharacter;
        // 캐릭터 프리펩
        Instantiate(data.prefab, transform);
        animator = GetComponentInChildren<Animator>();
        // 캐릭터 기본스킬
        SkillKind skillKind = data.firstSkill;
        AddOrIncreaseSkill(skillKind);
        // 캐릭터 사운드
        Sounds = data.Sounds;

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
        ProcessDamage();
    }

    protected void OnTriggerStay(Collider other)
    {

        Unit target = other.gameObject.GetComponent<Unit>();
        if (target == null) return;

        if (target.type != UnitType.Monster)
        {
            return;
        }

        if(damageFlag)
        {
            damageFlag = false;
            stat.TakeDamage(target.stat.Get_FinalStat(StatType.Strength));
        }
        
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

    void ProcessDamage()
    {
        damageTick += Time.fixedDeltaTime;
        if(damageTick > damageDelay)
        {
            damageTick = 0f;
            damageFlag = true;
        }
    }

    void OnDeadCallback()
    {
        GameManager.Instance.Pause();
        UI_Gameover.instance.Show();
        SoundManager.Instance.PlaySFXSound("Death");
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

        if(kind == SkillKind.RecoveryHp )
        {
            Skill skill = gameObject.AddComponent<Skill_RecoveryHp>();
            skill.Active();
            Destroy(skill);
            return;
        }
        if(kind==SkillKind.IncreaseCoin)
        {
            Skill skill = gameObject.AddComponent<Skill_IncreaseCoin>();
            skill.Active();
            Destroy(skill);
            return;
        }
        AddOrIncreaseSkill(kind);
    }

    private void Init_Stat()
    {
        DataManager dataManager = DataManager.Instance;
        Stat _stat = GetComponent<Stat>();
        _stat.Set_Stats(dataManager.playerCharacterData[(int)dataManager.currentGameData.characterName].playerCharacter.statsData.stats);
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

    

    List<SkillInformation> MakeNewSkillInformations()
    {
        List<SkillInformation> skillInfos = new List<SkillInformation>();

        List<Skill> actives = GetActiveSkills();
        List<Skill> passives = GetPassiveSkills();

        List<SkillKind> kinds = new List<SkillKind>();

        for (int index = (int)SkillKind.IceBolt; index < (int)SkillKind.RecoveryHp; index++)
        {
            SkillKind kind = (SkillKind)index;
            SkillType type = kind.GetSkillType();
            Skill skill = FindSkill(kind);
            SkillData data = DataManager.Instance.skillDatas[(int)kind].skillData;

            if (type == SkillType.Active && skill == null && actives.Count >= 6) continue;
            if (type == SkillType.Passive && skill == null && passives.Count >= 6) continue;
            if (skill != null && skill.IsMaxLevel) continue;
            if (skill == null && data.grade != Grade.Normal) continue;

            kinds.Add(kind);
            if (skill != null)
            {
                kinds.Add(kind);
                kinds.Add(kind);
            }
        }


        if (kinds.Count == 0)
        {
            // TODO : Make HP and Money
            kinds.Add(SkillKind.RecoveryHp);
            kinds.Add(SkillKind.IncreaseCoin);
            skillInfos.Add(new SkillInformation(SkillKind.RecoveryHp, 1));
            skillInfos.Add(new SkillInformation(SkillKind.IncreaseCoin, 1));
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
                kinds.RemoveAll(x => x == kind);
                count++;
            }
        }

        




        return skillInfos;
    }
    

}
