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
    BlackHole,
    Laser,
    FireTornado,
    RockTotem,
    ShurikenAttack,
    Lightning,
    End,
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
public enum Grade
{
    Normal, Magic, Rare, Unique, Legendary
}


[System.Serializable]
public class SkillValue
{
    public SkillLevel level;
    public string description;
    public float cooltime;
    public float damage;
    public float duration;
    public float delay;
    public float speed;
    public int amount;
}
