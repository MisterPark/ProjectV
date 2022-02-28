using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillKind
{
    None,
    IceBolt,
    FireBolt,
}

public enum SkillType
{
    Passive,
    Active
}


public abstract class Skill : MonoBehaviour
{
    public Sprite icon;
    public int level = 1;
    public int maxLevel = 8;
    public SkillKind Kind;
    public SkillType Type { get { return Kind.GetSkillType(); } }
    public float Cooltime = 0.5f;

    float tick = 0f;
    //public float 
    protected abstract void Active();
    protected virtual void Start()
    {
        
    }

    void Update()
    {
        tick += Time.deltaTime;
        if (tick >= Cooltime)
        {
            tick = 0f;
            Active();
        }
    }

}
