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
        // Ŀ�� ����
        Cursor.visible = false;
        // ������Ʈ Ǯ ����
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
