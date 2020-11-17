using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string Name;

    public bool RoundIsRunning = false;
    public bool PlayerIsDead = false;

    [SerializeField]
    CircleController circlePrefab;

    private SortedSet<CircleController> circles;

    private CameraController cameraController;

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
        cameraController = Camera.main.GetComponent<CameraController>();
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

        //add circle only after updating circle transform
        cameraController.AddCircle(newCircle.gameObject.transform);

    }

    public void RemoveCircle()
    {
        // find 
        //GameObject temp =
        //cameraController.RemoveCircle(temp.gameObject.transform);
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
