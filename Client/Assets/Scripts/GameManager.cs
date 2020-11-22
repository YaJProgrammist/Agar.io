﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    private const float SEND_DIRECTION_TIME = 0.2f;
    private float sendDirectionTimer = 0;

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
        CheckIfDivideCurrentPlayer();

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


    //put next methods into server connection class
    public void SendDirection()
    {
        var mousePos = Input.mousePosition;
        Vector2 direction = new Vector2(mousePos.x - Screen.width / 2, mousePos.y - Screen.height / 2).normalized;
        //send on server current player direction

        EventsSender.RegisterEvent(new ChangeVelocity(PlayerManager.Instance.currentPlayerId, (double)direction.x, (double)direction.y));
    }

    public void CheckIfDivideCurrentPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventsSender.RegisterEvent(new Split(PlayerManager.Instance.currentPlayerId));
            // send on server player want to split
        }
    }

    public void CheckIfLeaveGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CurrentPlayerLeft();
            EventsSender.RegisterEvent(new LeaveGame(PlayerManager.Instance.currentPlayerId));
            // send on server player left outgo 
        }
    }

    void OnApplicationQuit()
    {
        EventsSender.RegisterEvent(new LeaveGame(PlayerManager.Instance.currentPlayerId));
    }

    void FixedUpdate()
    {
        if (RoundIsRunning && !PlayerIsDead)
        {
            CheckIfLeaveGame();
            
            if (sendDirectionTimer >= SEND_DIRECTION_TIME)
            {
                SendDirection();
                sendDirectionTimer = 0;
            }
            else
            {
                sendDirectionTimer += Time.fixedDeltaTime;
            }
        }
    }
}
