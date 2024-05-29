using MoonActive.Connect4;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public enum CellStatus
{
    Empty,
    RedDisk,
    BlueDisk
}
public class GameGrid : MonoBehaviour, IGrid
{
    public static GameGrid Instance;

    void Awake()
    {
        Instance = this;
    }

    public List<int> AvailableCols;
    public bool Match4Found;
    public event Action<int> ColumnClicked;

    CellStatus[,] gridCells;
    Disk spawnedDisk;

    private void Start()
    {
        AvailableCols = new List<int>();
        gridCells = new CellStatus[7, 6];
    }

    public IDisk Spawn(Disk diskPrefab, int column, int row)
    {
        row = GetFirstAvailableRow(column);
        ColliderContainer.Instance.EnableColliderOnGrid(column, row);
        Vector2 spawnPosition = new Vector2(417 + 141 * column, 1537.40f);
        spawnedDisk = Instantiate(diskPrefab, spawnPosition, Quaternion.identity, transform);
        MarkCell(column, row);
        spawnedDisk.StoppedFalling += TurnEnded;
        return spawnedDisk;
    }

    void TurnEnded()
    {
        spawnedDisk.StoppedFalling -= TurnEnded;
        GameHandler.Instance.EndTurn();
    }

    int GetFirstAvailableRow(int column)
    {
        for(int y = 0; y < 6; y++)
        {
            if (gridCells[column, y] == CellStatus.Empty)
            {
                return y;
            }
        }

        //Should never happen in theory but just in case
        return -1;
    }

    void MarkCell(int col, int row) 
    {
        gridCells[col, row] = (GameHandler.Instance.CurrentTurn == GameTurn.BLUE) ? CellStatus.BlueDisk : CellStatus.RedDisk;
        CheckIfConnected(col, row);
        if (row == 5)
        {
            AvailableCols.Remove(col);
        }
    }

    private void CheckIfConnected(int col, int row)
    {
        CellStatus centerCell = gridCells[col, row];
        int rowCount = 0, colCount = 0, diagCount = 0;

        //Check Row
        for(int x = col - 3; x <= col + 3 && rowCount != 3; x++)
        {
            if (x < 0 || x > 6 || x == col) continue;
            int y = row;
            if (gridCells[x,y] == centerCell)
            {
                rowCount++;
            }
            else
            {
                rowCount = 0;
            }
        }
        if (rowCount == 3)
        {
            Match4Found = true;
            return;
        }

        //Check Column
        for (int y = row - 3; y <= row + 3 && colCount != 3; y++)
        {
            if (y < 0 || y > 5 || y == col) continue;
            int x = col;
            if (gridCells[x, y] == centerCell)
            {
                colCount++;
            }
            else
            {
                colCount = 0;
            }
        }
        if (colCount == 3)
        {
            Match4Found = true;
            return;
        }

        //Check diagonal going upright
        for(int offset = -3; offset <= 3 && diagCount != 3; offset++)
        {
            if (offset == 0) continue;

            int x = col + offset, y = row + offset;
            if (x < 0 || x > 6 || y < 0 || y > 5) continue;

            if (gridCells[x,y] == centerCell)
            {
                diagCount++;
            }
            else
            {
                diagCount = 0;
            }
        }
        if (diagCount == 3)
        {
            Match4Found = true;
            return;
        }

        diagCount = 0;

        //Check diagonal going downright
        for(int offset = -3; offset <= 3; offset++)
        {
            if (offset == 0) continue;

            int x = col + offset, y = row - offset;
            if (x < 0 || x > 6 || y < 0 || y > 5) continue;

            if (gridCells[x, y] == centerCell)
            {
                diagCount++;
            }
            else
            {
                diagCount = 0;
            }
        }
        if (diagCount == 3)
        {
            Match4Found = true;
            return;
        }
    }
}
