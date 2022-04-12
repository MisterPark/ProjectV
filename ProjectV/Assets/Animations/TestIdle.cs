using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIdle : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        animator.SetInteger("UnitType", (int)UnitType.Monster);
    }
}
