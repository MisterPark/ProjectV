using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Status : MonoBehaviourEx, IUpdater
{
    private Stat playerStat;
    private RectTransform rectTransform;
    [SerializeField] private UI_StatData[] children;
    
    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
    }

    
    public void UpdateEx()
    {
        playerStat = Player.Instance.stat;
        if (playerStat == null)
            return;
        foreach (UI_StatData child in children)
        {
            float value = playerStat.Get_FinalStat(child.statType);

            if(value == 0)
                child.value.text = "-";
            else
            {
                if(playerStat.Get_OriginStat(child.statType) == playerStat.Get_FinalStat(child.statType))
                {
                    child.value.color = Color.white;
                }
                else
                {
                    child.value.color = Color.yellow;
                }

                if (child.isPercent == false)
                {
                    child.value.text = (Mathf.Round(value * 10f) * 0.1f).ToString();
                }
                else
                {
                    child.value.text = Mathf.Round((value * 100f)).ToString() + "%";
                }
            }

        }
    }

}
