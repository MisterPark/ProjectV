using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    private int count = 16;
    private Stat playerStat;
    private RectTransform rectTransform;
    [SerializeField] private UI_StatData[] children;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerStat = Player.Instance.stat;
        if (playerStat == null)
            return;
        foreach (UI_StatData child in children)
        {
            float tempStat = playerStat.Get_FinalStat(child.statType);
            float value = tempStat;
            if(value == 0)
                child.value.text = "-";
            else
            {
                if(child.statType == StatType.MoveSpeed || child.statType == StatType.Recovery || child.statType == StatType.Armor || child.statType == StatType.Amount)
                {
                    child.value.text = value.ToString();
                }
                else
                    child.value.text = value.ToString() + "%";
            }

        }
    }

}
