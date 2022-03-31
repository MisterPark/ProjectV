using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviourEx {
	Image Filler;
	public Slider slider;

	// Use this for initialization
	void Start () {
		Filler = GetComponent<Image>();
	}
	
	
	void Update () {
		Filler.fillAmount = slider.value;
	}
}
