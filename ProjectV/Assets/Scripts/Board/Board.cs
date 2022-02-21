using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Row { get; set; }
    public int Column { get; set; }

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.IsPlayer())
        {
            Debug.Log($"{Row},{Column}");
        }
    }

    public bool IsFarFromThePlayer()
    {
        if (Row < Player.Row - 2) return true;
        if (Row > Player.Row + 2) return true;
        if (Column < Player.Column - 2) return true;
        if (Column > Player.Column + 2) return true;

        return false;
    }



}