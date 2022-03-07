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
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        int listcount = UIObjectList.Count;
        for (int listrepeat = 0; listrepeat < listcount; ++listrepeat)
        {
            UIObjectDic.Add(UIObjectList[listrepeat].name, UIObjectList[listrepeat]);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject GetUIObject(string objectname)
    {
        return UIObjectDic[objectname];
    }

    public void SetUIActive(string objectname, bool bset)
    {
        UIObjectDic[objectname].SetActive(bset);
    }

    public bool GetUIActive(string objectname)
    {
        return UIObjectDic[objectname].activeSelf; 
    }


}
