using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviourEx
{
    Material mat;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    
    public override void FixedUpdateEx()
    {
        float progress = LoadingSceneManager.instance.Progress;
        mat.SetFloat("_FillAmount", progress);
    }
}
