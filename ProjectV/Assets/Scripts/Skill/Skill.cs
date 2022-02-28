using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Skill : MonoBehaviour
{
    public Sprite icon;
    public int level = 1;
    public int maxLevel = 8;
    public SkillKind Kind;
    public SkillType Type { get { return Kind.GetSkillType(); } }
    public float cooltime = 0.5f;
    public float damage;
    public int amount = 1;

    float tick = 0f;

    protected abstract void Active();
    protected virtual void Start()
    {
        
    }

    void FixedUpdate()
    {
        tick += Time.fixedDeltaTime;
        if (tick >= cooltime)
        {
            tick = 0f;
            for(int i = 0; i < amount; i++)
            {
                Active();
            }
            
        }
    }

}
