using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public List<GameObject> UIObjectList; 
    
    private Dictionary<string, GameObject> UIObjectDic = new Dictionary<string, GameObject>();

    public string StartSceneName;



}
