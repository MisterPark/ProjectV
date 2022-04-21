using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RenderManager : MonoBehaviourEx
{
    private static RenderManager instance;
    public static RenderManager Instance
    {
        get { return instance; }
    }

    [SerializeField] private ForwardRendererData rendererData = null;

    protected override void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool TryGetFeature(out ScriptableRendererFeature feature, string featureName)
    {
        feature = rendererData.rendererFeatures.Where((f)=> f.name == featureName).FirstOrDefault();
        return feature != null;
    }

    public void EnableFeature(string featureName)
    {
        ScriptableRendererFeature feature = null;
        if (TryGetFeature(out feature, featureName))
        {
            feature.SetActive(true);
        }
    }

    public void DisableFeature(string featureName)
    {
        ScriptableRendererFeature feature = null;
        if(TryGetFeature(out feature, featureName))
        {
            feature.SetActive(false);
        }
    }

    public void SetShadow(bool _enable)
    {
        ScriptableRendererFeature feature = null;
        if (TryGetFeature(out feature, "Shadow"))
        {
            feature.SetActive(_enable);
        }
    }
}
