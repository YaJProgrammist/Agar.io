using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string Name;

    public bool RoundIsRunning;
    public bool PlayerIsDead;

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

    EventsSender.RegisterEvent(new ConnectionToServer());
        StartGame();
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
    }

    public void DivideCurrentPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // send on server player want to split
        }
    }

    public void LeaveGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CurrentPlayerLeft();
            // send on server player left outgo 
        }
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
