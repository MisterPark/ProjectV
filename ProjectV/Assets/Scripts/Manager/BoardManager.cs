using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviourEx, IFixedUpdater
{
    public static BoardManager Instance;
    [SerializeField]List<Board> boards = new List<Board>();


    private bool pauseFlag = false;
    public bool Pause { get { return pauseFlag; } set { pauseFlag = value; } }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        InitializeBoard();
    }

    public void FixedUpdateEx()
    {
        CreateBoards();
        RemoveBoards();
    }

    void InitializeBoard()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < 3; i++) 
        {
            for (int j = 0; j < 3; j++)
            {
                CreateBoard(i-1, j-1);
            }
        }

    }

    public List<Board> GetBoards()
    {
        return boards;
    }

    bool FindBoard(int row, int col)
    {
        var boardArray = boards.ToArray();
        for (int i = 0; i < boardArray.Length; i++)
        {
            Board board = boardArray[i];
            if (board.Row == row && board.Column == col)
            {
                return true;
            }
        }

        return false;
    }

    void CreateBoard(int row, int col)
    {
        if (FindBoard(row, col)) return;

        GameObject boardObj = ObjectPool.Instance.Allocate("Terrain3");
        Board board = Board.Find(boardObj);
        board.Row = row;
        board.Column = col;

        // 초기 위치 세팅
        float x = col * 100f;
        float y = row * 100f;

        boardObj.transform.position = new Vector3(x, 0, y);
        boards.Add(board);
    }

    void CreateBoards()
    {
        if (Pause) return;
        if (Player.Instance == null) return;

        int row = Player.Row;
        int col = Player.Column;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                CreateBoard(row + i, col + j);
            }
        }
    }

    private void RemoveBoards()
    {
        if (Player.Instance == null) return;

        Vector3 playerPos = Player.Instance.transform.position;

        List<Board> removes = new List<Board>();
        int count = boards.Count;
        for (int i = 0; i < count; i++) 
        {
            Board board = boards[i];
            if(board.IsFarFromThePlayer())
            {
                removes.Add(board);
            }
        }

        for (int i = 0; i < removes.Count; i++)
        {
            Board board = removes[i];
            boards.Remove(board);
            ObjectPool.Instance.Free(board.gameObject);
        }
    }


}
