using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Togglescript : MonoBehaviourEx, IUpdater
{

    Toggle toggle;

    protected override void Start()
    {
        base.Start();
        toggle = GetComponent<Toggle>();
    }

    public GameObject Slider;


    public void UpdateEx()
    {
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
