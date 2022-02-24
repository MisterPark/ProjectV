using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0f;
    public bool isRotate=false;
    public Item Item { get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotate)
        transform.Rotate(Vector3.up, rotationSpeed,Space.World);
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
