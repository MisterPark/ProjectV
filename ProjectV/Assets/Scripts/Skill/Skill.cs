using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    None,
    IceBolt,
    FireBolt,
}

public abstract class Skill : MonoBehaviour
{
    public int level = 1;
    public SkillType Type;
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
