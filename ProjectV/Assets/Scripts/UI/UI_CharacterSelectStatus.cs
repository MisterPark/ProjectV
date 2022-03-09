using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterSelectStatus : MonoBehaviour
{
    private Stats[] playerStat;
    private RectTransform rectTransform;
    [SerializeField] private UI_StatData[] children;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        playerStat = DataManager.Instance.playerCharacterData[(int)(DataManager.Instance.currentGameData.characterName)].statsData.stats;
    }

    // Update is called once per frame
    void Update()
    {
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
                    child.value.color = Color.black;
                }
                if (child.isPercent == false)
                {
                    child.value.text = value.ToString();
                }
                else
                {
                    child.value.text = value.ToString() + "%";
                }
            }

        }
    }
}
