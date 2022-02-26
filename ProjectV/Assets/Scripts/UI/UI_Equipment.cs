using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
{

    private RectTransform rectTransform;
    [SerializeField] private RectTransform background;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
