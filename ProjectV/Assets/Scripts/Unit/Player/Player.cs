using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        OnDead.AddListener(OnDeadCallback);
        // TODO : 임시
        gameObject.AddComponent<Skill_IceBalt>();
    }

    protected override void Update()
    {
        base.Update();
        Move();
        //Debug.Log($"{Column},{Row} / {transform.position}");
        
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

    }
}
