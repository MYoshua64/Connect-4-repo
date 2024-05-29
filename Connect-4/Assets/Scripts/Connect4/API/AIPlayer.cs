using MoonActive.Connect4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public static AIPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Disk redDiskPrefab;
    [SerializeField] Disk blueDiskPrefab;

    public void EnableAI()
    {
        GameHandler.Instance.OnTurnSwitch += CheckIfCanMove;
    }

    void CheckIfCanMove()
    {
        if (GameHandler.Instance.CurrentTurn == GameTurn.RED)
        {
            MakeMove();
        }
    }

    void MakeMove()
    {
        int randomCol = Random.Range(0, GameGrid.Instance.AvailableCols.Count);
        GameGrid.Instance.Spawn(GameHandler.Instance.CurrentPlayerDisk, randomCol, 0);
    }

    public void DisableAI()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
