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
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

        else if (MagnetFlag)
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
