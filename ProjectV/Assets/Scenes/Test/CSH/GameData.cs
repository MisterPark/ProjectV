using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // 직렬화

public class GameData 
{ 
    // 각 챕터의 잠금여부
    public bool[] unLock_Character = new bool[(int)CharacterName.END];
    public bool[] unLock_Stage = new bool[3];
}