using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviourEx, IFixedUpdater
{
    Material mat;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        mat = GetComponent<Renderer>().material;
    }

    
    public void FixedUpdateEx()
    {
        float progress = LoadingSceneManager.instance.Progress;
        mat.SetFloat("_FillAmount", progress);

    }
}
