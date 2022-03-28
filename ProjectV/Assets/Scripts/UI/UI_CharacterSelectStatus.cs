using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterSelectStatus : UI
{
    private Stats[] playerStat;
    private RectTransform rectTransform;
    [SerializeField] private UI_StatData[] children;
    // Start is called before the first frame update
    static public UI_CharacterSelectStatus Instance;
    void Start()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        playerStat = DataManager.Instance.playerCharacterData[(int)(DataManager.Instance.currentGameData.characterName)].playerCharacter.statsData.stats;
    }


    private void OnEnable()
    {
        Setting_UIStatus();
    }

    public void Setting_UIStatus()
    {
        if (DataManager.Instance == null)
            return;
        playerStat = DataManager.Instance.playerCharacterData[(int)(DataManager.Instance.currentGameData.characterName)].playerCharacter.statsData.stats;
        if (playerStat == null)
            return;

        foreach (UI_StatData child in children)
        {
            float tempStat = playerStat[(int)(child.statType)].origin_Stat;
            float tempPowerupStat = DataManager.Instance.powerUpStat[(int)(child.statType)];
            float value = tempStat + tempPowerupStat;
            if ((tempStat == 0) && (tempPowerupStat == 0))
            {
                child.value.text = "-";
                child.value.color = Color.white;
            }
            else
            {
                if (tempPowerupStat != 0)
                {
                    child.value.color = Color.yellow;
                }
                else
                {
                    child.value.color = Color.white;
                }
                if (child.isPercent == false)
                {
                    if(child.statType == StatType.MaxHealth || child.statType == StatType.Armor || child.statType == StatType.Amount)
                    {
                        child.value.text = value.ToString();
                        continue;
                    }
                    float oriValue = DataManager.Instance.playerCharacterData[(int)PlayerCharacterName.Witch].playerCharacter.statsData.stats[(int)child.statType].origin_Stat;
                    float differenceValue = value - oriValue;
                    if(differenceValue == 0f)
                    {
                        child.value.text = "-";
                    }
                    else
                    {
                        float percentValue = Mathf.Round(differenceValue * 100f / oriValue);
                        if (percentValue > 0f) child.value.text = "+" + (percentValue).ToString() + "%";
                        else child.value.text = (percentValue).ToString() + "%";
                    }
                }
                else
                {
                    if (value == 1f)
                    {
                        child.value.text = "-";
                    }
                    else
                    {
                        string valueText = Mathf.Round(((value - 1) * 100f)).ToString() + "%";
                        if (value > 1f) valueText = "+" + valueText;
                        child.value.text = valueText;
                    }
                }
            }

        }
    }
}

