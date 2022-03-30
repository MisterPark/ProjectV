using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Togglescript : MonoBehaviourEx {

    Toggle toggle;

    protected override void Start()
    {
        base.Start();
        toggle = GetComponent<Toggle>();
    }

    public GameObject Slider;


    public override void UpdateEx()
    {
        base.UpdateEx();
        if (toggle.isOn)
        {
            Slider.SetActive(false);
        }
        else
        {
            Slider.SetActive(true);
        }
    }
}
