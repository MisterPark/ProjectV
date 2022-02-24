using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    public UnitStatData statData;

    [SerializeField] private UI_StatData[] children;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(UI_StatData child in children)
        {
            bool isFind = false;
            foreach (Stats stat in statData.stats)
            {
                if (child.statType == stat.statType)
                {
                    float value = stat.final_Stat;
                    if(value == 0)
                        child.value.text = "-";
                    else
                        child.value.text = value.ToString();
                    isFind = true;
                }
            }
            if (isFind == false)
                child.value.text = "-";
        }
    }
}
