using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SkillKind
{
    None,
    IceBolt,
    FireBolt,
    ForceFieldBarrier,
}
[System.Serializable]
public enum SkillType
{
    Passive,
    Active
}
[System.Serializable]
public enum SkillLevel
{
    Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, End
}

[System.Serializable]
public class SkillValue
{
    public SkillLevel level;
    public Sprite icon;
    public SkillKind kind;
    public SkillType type;
    public float cooltime;
    public float damage;
    public int amount;
}
