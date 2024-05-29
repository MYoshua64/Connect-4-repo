using MoonActive.Connect4;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums
//Enum to help track who's turn it is
public enum GameTurn
{
    RED,
    BLUE
}

//Enum to determine the game mode
[Serializable]
public enum GameMode 
{
    PvE = 0,
    PvP = 1
}
#endregion

public class GameHandler : MonoBehaviour
{
    #region Singleton
    public static GameHandler Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
    #endregion

    public GameTurn CurrentTurn { get; private set; }
    public GameMode currentGameMode { get; private set; }
    public Action OnTurnSwitch;

    [SerializeField] Disk redDiskPrefab;
    [SerializeField] Disk blueDiskPrefab;


    private void Start()
    {
        CurrentTurn = GameTurn.BLUE;
    }

    public void EndTurn()
    {
        if (!GameGrid.Instance.Match4Found)
        {
            CurrentTurn = (CurrentTurn == GameTurn.BLUE) ? GameTurn.RED : GameTurn.BLUE;
            OnTurnSwitch?.Invoke();
            Debug.Log(CurrentTurn);
        }
        else //Win condition was met, end the game
        {
            Debug.Log((CurrentTurn == GameTurn.BLUE) ? "Blue won!" : "Red Won!");
        }
    }

    public void SetGameMode(int mode)
    {
        currentGameMode = (GameMode)mode;
    }

    public void StartGame()
    {
        if (currentGameMode == GameMode.PvP)
        {
            AIPlayer.Instance?.DisableAI();
        }
        else
        {
            AIPlayer.Instance?.EnableAI();
        }
    }

    public Disk CurrentPlayerDisk
    {
        get
        {
            return (CurrentTurn == GameTurn.RED) ? redDiskPrefab : blueDiskPrefab;
        }
    }
}
