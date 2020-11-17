using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CircleController circlePrefab;

    private SortedSet<CircleController> circles;

    public string Name;

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
        circles = new SortedSet<CircleController>();
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        UIManager.Instance.ShowStartMenu();

        //create main circle
        CircleController circle = Instantiate(circlePrefab);
        //AddCircle(circle);
        //circle.SetPlayerStartValues();
    }

    public void AddPlayer()
    {
        // send on new player server and wait till round will start
        UIManager.Instance.ShowWaitingWindow();
    }

    public void AddCircle()
    {
        CircleController newCircle = Instantiate(circlePrefab);
        //newCircle.GetComponent<CircleController>().SetPlayerStartValues();

    }

    public void RemoveCircle()
    {
        // find 
        //GameObject temp =
        //remove from sorted set
        // call .killcircle() on temp
    }

    public void PlayerLeft()
    {
        UIManager.Instance.ShowPlayerLeftWindow();
    }

    public void StartRound()
    {
        RoundIsRunning = true;

        UIManager.Instance.CloseAllWindows();
    }

    public void EndRound()
    {
        RoundIsRunning = false;
        //UIManager.Instance.ShowAndUpdateRating();
    }

}
