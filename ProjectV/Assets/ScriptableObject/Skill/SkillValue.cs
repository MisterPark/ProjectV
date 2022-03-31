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
    PoisonTrail,
    Shuriken,
    Lightning,
    Rain,
    Strength,
    Armor,
    MaxHp,
    Range,
    Speed,
    MoveSpeed,
    Magnet,
    Growth,
    FrozenOrb,
    UnstableMagicMissile,
    HeavyFireBall,
    Meteor,
    IceTornado,
    PoisonTornado,
    WindTornado,
    LavaOrb,
    PoisonNova,
    CoolDown,
    //caution : active or passive skill add on this text;
    RecoveryHp,
    IncreaseCoin,
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
    public float range;
    public int amount;
}
