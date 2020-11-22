using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool RoundIsRunning;
    public bool PlayerIsDead;
    public Queue<IncomingGameEvent> GameEvents = new Queue<IncomingGameEvent>(); 

    public static event EventHandler RoundEnded;

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Update()
    {
        //CheckIfDivideCurrentPlayer();

        while (GameEvents.Count > 0)
        {
            GameEvents.Dequeue().Handle();
        }
    }

    private void InitializeManager()
    {

    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }

    public void Start()
    {
        RoundIsRunning = false;
        PlayerIsDead = false;

        StartGame();
    }

    public void SetConnection()
    {
        EventsSender.RegisterEvent(new ConnectionToServer());
    }

    public void StartGame()
    {
        UIManager.Instance.ShowStartMenu();
    }

    public void CurrentPlayerLeft()
    {
        RoundIsRunning = true;
        PlayerIsDead = true;

        UIManager.Instance.ShowPlayerLeftWindow();
        PlayerManager.Instance.KillCurrentPlayer();
    }

    public void StartRound()
    {
        RoundIsRunning = true;

        UIManager.Instance.CloseAllWindows();

        //StartCoroutine(UpdatePlayerVelocity());
    }

    public void EndRound()
    {
        RoundIsRunning = false;
        PlayerIsDead = true;

        RoundEnded?.Invoke(this, new EventArgs());
    }


    void OnApplicationQuit()
    {
        EventsSender.RegisterEvent(new LeaveGame(PlayerManager.Instance.currentPlayerId));
    }

}
