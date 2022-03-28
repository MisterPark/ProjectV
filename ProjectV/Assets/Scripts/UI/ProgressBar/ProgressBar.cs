using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    Material mat;

    private void Awake()
    {
        
    }
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        float progress = LoadingSceneManager.instance.Progress;
        mat.SetFloat("_FillAmount", progress);
    }
}
