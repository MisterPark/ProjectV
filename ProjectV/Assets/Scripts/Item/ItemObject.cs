using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviourEx
{
    [SerializeField] float rotationSpeed = 0f;
    float moveSpeed = 15f;
    public Item Item { get; set; }
    public bool isRotate { get; set; } = false;
    public bool isMagnetism { get; set; } = false;
    public bool MagnetFlag { get; set; } = false;
    public bool isChest { get; set; } = false;
    public bool SinWaveFlag { get; set; } = false;

    public static Dictionary<GameObject, ItemObject> ItemObjects = new Dictionary<GameObject, ItemObject>();

    protected override void Awake()
    {
        base.Awake();
        ItemObjects.Add(gameObject, this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ItemObjects.Remove(gameObject);
    }
    public static ItemObject Find(GameObject obj)
    {
        ItemObject itemObject;
        if (ItemObjects.TryGetValue(obj, out itemObject) == false)
        {
            Debug.LogError("Unregistered itemObject");
        }
        return itemObject;
    }

    public override void FixedUpdateEx()
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
