using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterSelectStatus : MonoBehaviour
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
        playerStat = DataManager.Instance.playerCharacterData[(int)(DataManager.Instance.currentGameData.characterName)].statsData.stats;
    }


    private void OnEnable()
    {
        Setting_UIStatus();
        Debug.Log("Setting");
    }

    public void Setting_UIStatus()
    {
        if (DataManager.Instance == null)
            return;
        playerStat = DataManager.Instance.playerCharacterData[(int)(DataManager.Instance.currentGameData.characterName)].statsData.stats;
        if (playerStat == null)
            return;

        foreach (UI_StatData child in children)
        {
            float tempStat = playerStat[(int)(child.statType)].origin_Stat;
            float tempPowerupStat = DataManager.Instance.powerUpStat[(int)(child.statType)];
            float value = tempStat + tempPowerupStat;
            if ((tempStat == 0) && (tempPowerupStat == 0))
                child.value.text = "-";
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
                    child.value.text = value.ToString();
                }
                else
                {
                    child.value.text = (value * 100f).ToString() + "%";
                }
            }

        }
    }
}

