using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // ����ȭ

public class GameData 
{ 
    // �� é���� ��ݿ���
    public bool[] unLock_Character = new bool[(int)CharacterName.END];
    public bool[] unLock_Stage = new bool[3];
}