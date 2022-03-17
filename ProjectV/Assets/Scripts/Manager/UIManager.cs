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
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InitUIManager();
        int listcount = UIObjectList.Count;
        for (int listrepeat = 0; listrepeat < listcount; ++listrepeat)
        {
            UIObjectDic.Add(UIObjectList[listrepeat].name, UIObjectList[listrepeat]);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Stat stat = Player.Instance.GetComponent<Stat>();
            stat.Increase_FinalStat(StatType.Exp, 5000);
        }
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

    private void InitUIManager()
    {
        if(UIObjectList.Count == 0)
        {
            GameObject mainGameCanvas = GameObject.Find("MainGame Canvas");
            if (mainGameCanvas == null)
                return;
            UIObjectList.Add(mainGameCanvas);
            int childCount = mainGameCanvas.transform.childCount;
            for(int i = 0; i < childCount; i++)
            {
                UIObjectList.Add(mainGameCanvas.transform.GetChild(i).gameObject);
            }
        }
    }


}
