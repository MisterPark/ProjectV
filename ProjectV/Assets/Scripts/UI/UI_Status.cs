using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
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
            float value = playerStat.Get_TotalPercentStat(child.statType);

            if(value == 0)
                child.value.text = "-";
            else
            {
                if(playerStat.Get_OriginStat(child.statType) == playerStat.Get_FinalStat(child.statType))
                {
                    child.value.color = Color.black;
                }
                else
                {
                    child.value.color = Color.yellow;
                }

                if(child.isPercent == false)
                {
                    child.value.text = value.ToString();
                }
                else
                    child.value.text = (value * 100f).ToString() + "%";
            }

        }
    }

}
