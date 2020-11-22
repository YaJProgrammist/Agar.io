using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteractionManager : MonoBehaviour
{
    private const float SEND_DIRECTION_TIME = 0.2f;
    private float sendDirectionTimer = 0;

    public static UserInteractionManager Instance { get; private set; }

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
            GameManager.Instance.CurrentPlayerLeft();
            EventsSender.RegisterEvent(new LeaveGame(PlayerManager.Instance.currentPlayerId));
            // send on server player left outgo 
        }
    }

    void FixedUpdate()
    {

        if (GameManager.Instance.RoundIsRunning && !GameManager.Instance.PlayerIsDead)
        {
            CheckIfLeaveGame();

            CheckIfDivideCurrentPlayer();

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
