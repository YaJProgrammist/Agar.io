using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool RoundIsRunning = false;
    public bool PlayerIsDead = false;

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
        EventsSender.RegisterEvent(new ConnectionToServer());
        StartGame();
    }

    public void StartGame()
    {
        UIManager.Instance.ShowStartMenu();
    }

    public void AddPlayer()
    {
        // send on new player server and wait till round will start
        UIManager.Instance.ShowWaitingWindow();
    }

    public void PlayerLeft()
    {
        UIManager.Instance.ShowPlayerLeftWindow();
    }

    public void StartRound()
    {
        RoundIsRunning = true;

        //if we don't need to show player score, time or any other results, we just close all windows and menu
        UIManager.Instance.CloseAllWindows();
    }

    public void EndRound()
    {
        RoundIsRunning = false;
        //UIManager.Instance.ShowAndUpdateRating();
    }

}
