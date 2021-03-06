using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadingSceneManager : MonoBehaviourEx, IFixedUpdater
{
    public static LoadingSceneManager instance;

    public UnityEvent Completed;

    public string NextScene { get; set; }
    public float Progress { get; private set; }

    public float WaitTime { get; set; } = 5f; 
    float loadTick = 0;
    bool loadingFlag = false;
    bool trigger = false;

    AsyncOperation operation;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void FixedUpdateEx()
    {
        //Debug.Log($"{NextScene} {loadingFlag} {trigger} {loadTick} / {WaitTime}");
        if (loadingFlag == false) return;

        if(trigger == false)
        {
            trigger = true;
            
        }

        loadTick += Time.fixedDeltaTime;
        float progress = loadTick / WaitTime;
        Progress = progress > 1f ? 1f : progress;
        if (loadTick >= WaitTime)
        {
            Progress = 1;
            loadTick = 0;
            loadingFlag = false;
            operation = SceneManager.LoadSceneAsync(NextScene);
            operation.allowSceneActivation = true;
        }

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //Debug.Log("로딩씬매니저 삭제");
    }

    public void LoadScene(string sceneName, float waitTime = 5f)
    {
        NextScene = sceneName;
        Progress = 0f;
        WaitTime = waitTime;
        loadTick = 0;
        loadingFlag = true;
        trigger = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("LoadingScene");
    }

}
