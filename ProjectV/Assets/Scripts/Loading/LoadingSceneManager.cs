using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager instance;

    public UnityEvent Completed;

    public string NextScene { get; set; }
    public float Progress { get; private set; }

    public float WaitTime { get; set; } = 5f; 
    float loadTick = 0;
    bool isCompleted = false;
    bool loadingFlag = false;
    bool trigger = false;

    AsyncOperation operation;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (loadingFlag == false) return;

        if(trigger == false)
        {
            trigger = true;
            operation = SceneManager.LoadSceneAsync(NextScene);
            operation.allowSceneActivation = false;
        }

        loadTick += Time.fixedDeltaTime;
        float progress = loadTick / WaitTime;
        Progress = progress > 1f ? 1f : progress;
        if (loadTick >= WaitTime)
        {
            Progress = 1;
            loadTick = 0;
            loadingFlag = false;
            operation.allowSceneActivation = true;
        }

    }

    public void LoadScene(string sceneName, float waitTime = 5f)
    {
        NextScene = sceneName;
        Progress = 0f;
        WaitTime = waitTime;
        loadTick = 0;
        loadingFlag = true;
        trigger = false;
        SceneManager.LoadScene("LoadingScene");
        //StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(NextScene);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            loadTick += Time.deltaTime;
            float progress = loadTick / WaitTime;
            Progress = progress > 1f ? 1f : progress;
            if (loadTick > WaitTime)
            {
                Progress = 1;
                loadTick = 0;
                operation.allowSceneActivation = true;
            }
        }

        yield return null;
    }
}
