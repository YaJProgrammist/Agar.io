using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    StartMenuController startMenuController;

    [SerializeField]
    GameObject waitingWindow;

    [SerializeField]
    GameObject playerLeftWindow;

    RoundEndedMenuController roundEndedMenuController;

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
        startMenuController = GetComponent<StartMenuController>();
        roundEndedMenuController = GetComponent<RoundEndedMenuController>();
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }

    private void Start()
    {
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        startMenuController.Open();
        waitingWindow.SetActive(false);
        playerLeftWindow.SetActive(false);
        roundEndedMenuController.Close();
    }

    public void ShowWaitingWindow()
    {
        startMenuController.Close();
        waitingWindow.SetActive(true);
    }

    public void CloseAllWindows()
    {
        startMenuController.Close();
        waitingWindow.SetActive(false);
        playerLeftWindow.SetActive(false);
        roundEndedMenuController.Close();
    }

    public void ShowPlayerLeftWindow()
    {
        playerLeftWindow.SetActive(true);
    }

    public void ShowAndUpdateRating(List<int> playersId, List<double> playerScore)
    {
        playerLeftWindow.SetActive(false);

        roundEndedMenuController.Open();
        roundEndedMenuController.UpdateRating(playersId, playerScore);
    }

}
