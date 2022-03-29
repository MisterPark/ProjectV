using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0f;
    float moveSpeed = 15f;
    public Item Item { get; set; }
    public bool isRotate { get; set; } = false;
    public bool isMagnetism { get; set; } = false;
    public bool MagnetFlag { get; set; } = false;
    public bool isChest { get; set; } = false;
    public bool SinWaveFlag { get; set; } = false;
    public float LifeTime { get; set; }
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        LifeTime += Time.fixedDeltaTime;

        if(SinWaveFlag)
        {
            if(Item != null)
            {
                Vector3 itemPos = Item.gameObject.transform.localPosition;
                itemPos.y = (Mathf.Sin(LifeTime * 2f) + 1f) * 0.5f + 0.3f;
                Item.gameObject.transform.localPosition = itemPos;
            }
        }

        if(isRotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed, Space.World);
        }
        
        Vector3 to = Player.Instance.transform.position - transform.position;

        if (!MagnetFlag)
        {
            float radius = Player.Instance.stat.Get_FinalStat(StatType.Magnet);
            float dist = to.magnitude;
            if (dist <= radius)
            {
                MagnetFlag = true;
            }
        }

        else if (MagnetFlag && isChest == false)
        {
            transform.position += to.normalized * moveSpeed * Time.fixedDeltaTime;
        }
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.IsPlayer())
            return;

        Item?.Use();

        ItemManager.Instance.Remove(gameObject);
    }
    public void SetRotate(bool _isRotate) { isRotate = _isRotate; }
}
