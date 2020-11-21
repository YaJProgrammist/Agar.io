using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool RoundIsRunning;
    public bool PlayerIsDead;
    public Queue<IncomingGameEvent> GameEvents = new Queue<IncomingGameEvent>();

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

    }

    public void EndRound()
    {
        RoundIsRunning = false;
        PlayerIsDead = true;
        PlayerManager.Instance.KillEveryone();
    }


    //put next methods into server connection class
    public void SendDirection()
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Vector3.zero).normalized;
        //send on server current player direction

        EventsSender.RegisterEvent(new ChangeVelocity(PlayerManager.Instance.currentPlayerId, (double)direction.x, (double)direction.y));
    }

    public void DivideCurrentPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventsSender.RegisterEvent(new Split(PlayerManager.Instance.currentPlayerId));
            // send on server player want to split
        }
    }

    public void LeaveGame()
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (RoundIsRunning && !PlayerIsDead)
        {
            DivideCurrentPlayer();
            SendDirection();
            LeaveGame();
        }
    }

}
