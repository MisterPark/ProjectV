using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class OAuthManager : MonoBehaviourEx
{
    string log;

    private static OAuthManager instance;
    private GPGSBinder gpgsBinder;
    public Text myLog;
    private bool bWaitingForAuth = false;

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
    protected override void Start()
    {
        base.Start();
        //Debug.Log("Check");
        doAutoLogin();
    }
    public void doAutoLogin()
    {
        //Debug.Log("...");
        if (bWaitingForAuth)
            return;

        //���� �α����� �Ǿ����� �ʴٸ� 
        if (!Social.localUser.authenticated)
        {
           //Debug.Log("Authenticating...");
            bWaitingForAuth = true;
            //�α��� ���� ó������ (�ݹ��Լ�)
            Social.localUser.Authenticate(AuthenticateCallback);
        }
        else
        {
            //Debug.LogError("Login Fail\n");
        }
    }
    void AuthenticateCallback(bool success)
    {
        //Debug.Log("Loading");
        if (success)
        {
            // ����� �̸��� ����� 
            //Debug.Log("Welcome" + Social.localUser.userName + "\n");

        }
        else
        {
            //Debug.LogError("Login Fail\n");
        }
    }
}
