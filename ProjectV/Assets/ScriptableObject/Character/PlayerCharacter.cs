using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "PlayerCharacter", menuName = "Data/PlayerCharacter"), System.Serializable]
public class PlayerCharacter : ScriptableObject
{
    public PlayerCharacterName characterName;
    public UnitStatData statsData;
    public Sprite charImage;
    public SkillKind firstSkill;
    public string description;
    public GameObject prefab;
#if UNITY_EDITOR
    [ArrayElementTitle("kind")]
#endif
    public Sound[] Sounds = new Sound[(int)SoundKind.End];

    private void OnValidate()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].kind = (SoundKind)i;
        }
    }
}

[System.Serializable]
public enum PlayerCharacterName
{
    Witch,
    Wizard,
    Druid,
    Gypsy,
    Peasant,
    Sorcerer,
    Knight,
    Alchemist,
    King,
    Queen,
    Rogue,
    Cheater,
    END
}

[System.Serializable]
public class PlayerCharacterData
{
    public PlayerCharacterName name;
    public PlayerCharacter playerCharacter;
}