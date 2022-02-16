using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private Vector3 direction = Vector3.forward;// 캐릭터가 바라보는 방향, 스킬 사용시 사용 
    float moveSpeed = 5.0f;
    protected override void Start()
    {
        base.Start();
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
        transform.position += moveSpeed * moveDirection * Time.deltaTime;
        // Rotate
        transform.LookAt(transform.position + direction);

        bool isRun = moveDirection.magnitude > 0.01f ? true : false;
        animator.SetBool("IsRun", isRun);
    }
}
