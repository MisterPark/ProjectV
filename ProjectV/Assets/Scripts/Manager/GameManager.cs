using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject objectPoolPrefab;
    ObjectPool objectPool;
    public ObjectPool ObjectPool { get { return objectPool; } }
    private void Awake()
    {
        // 커서 숨김
        Cursor.visible = false;
        // 오브젝트 풀 생성
        GameObject poolObj = Instantiate(objectPoolPrefab);
        poolObj.name = "ObjectPool";
        objectPool = poolObj.GetComponent<ObjectPool>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
        }
    }
}
