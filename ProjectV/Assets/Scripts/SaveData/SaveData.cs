using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // ����ȭ

public class SaveData 
{ 
    // �� é���� ��ݿ���
    public bool[] unLockCharacter = new bool[(int)CharacterName.END];
    public bool[] unLockStage = new bool[3];

    public int totalGold;
    public int currentGold;
}