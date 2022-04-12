using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OAuthManager : MonoBehaviourEx
{
    string log;

    private static OAuthManager instance;
    private GPGSBinder gpgsBinder;
    public static OAuthManager Instance { get { return instance; } }

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            gpgsBinder = GPGSBinder.Inst;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}
