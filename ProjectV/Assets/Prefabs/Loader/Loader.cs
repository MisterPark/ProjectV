using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviourEx
{
    protected override void Start()
    {
        base.Start();
        SaveDataManager.Instance.LoadGameData();
    }

}
