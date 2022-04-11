using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviourEx
{
    public int Row { get; set; }
    public int Column { get; set; }

    public static Dictionary<GameObject, Board> Boards = new Dictionary<GameObject, Board>();

    protected override void Awake()
    {
        base.Awake();
        Boards.Add(gameObject, this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Boards.Remove(gameObject);
    }
    public static Board Find(GameObject obj)
    {
        Board board;
        if (Boards.TryGetValue(obj, out board) == false)
        {
            Debug.LogError("Unregistered board");
        }
        return board;
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
