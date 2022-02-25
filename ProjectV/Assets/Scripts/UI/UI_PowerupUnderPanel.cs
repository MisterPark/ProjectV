using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PowerupUnderPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_PowerupName;
    [SerializeField] private Image m_PowerupImage;
    [SerializeField] private TMPro.TextMeshProUGUI m_PowerupExplan;

    private string Name;
    private string Tip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_PowerupName.text = Name;
        m_PowerupExplan.text = Tip;
    }

    public void PowerupExplanInit(Powerup_DataType data)
    {
        Name = data.Powerup_Name;
        Tip = data.Powerup_Tip;
    }
}
