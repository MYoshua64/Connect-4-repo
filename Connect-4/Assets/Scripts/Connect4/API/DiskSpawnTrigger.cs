using MoonActive.Connect4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawnTrigger : MonoBehaviour
{
    static int columnCount;
    int column;
    int row;
    static bool canPlaceDisk = true;

    private void Start()
    {
        column = columnCount;
        columnCount = (columnCount + 1) % 7;
        GameHandler.Instance.OnTurnSwitch += HandleTurnSwitch;
        GameGrid.Instance.AvailableCols.Add(column);
        canPlaceDisk = true;
    }
    public void TriggerDiskSpawn()
    {
        if (!canPlaceDisk) return;
        if (!GameGrid.Instance.AvailableCols.Contains(column)) return;
        canPlaceDisk = false;
        GameGrid.Instance.Spawn(GameHandler.Instance.CurrentPlayerDisk, column, row);
    }

    void HandleTurnSwitch()
    {
        canPlaceDisk = 
            GameHandler.Instance.currentGameMode == GameMode.PvP || 
            (GameHandler.Instance.currentGameMode == GameMode.PvE && GameHandler.Instance.CurrentTurn == GameTurn.BLUE);
    }
}
