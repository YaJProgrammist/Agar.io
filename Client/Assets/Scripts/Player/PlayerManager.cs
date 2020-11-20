using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public string currentPlayerName;
    public int currentPlayerId;

    [SerializeField]
    GameObject currentPlayerContainer;

    [SerializeField]
    GameObject otherPlayersContainer;

    [SerializeField]
    CircleController circlePrefab;

    public SortedSet<CircleController> currentPlayerCircles;

    private SortedSet<CircleController> otherPlayersCircles;

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
        currentPlayerCircles = new SortedSet<CircleController>();
        otherPlayersCircles = new SortedSet<CircleController>();
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }

    public void UpdateCircles(List<int> playerId, List<List<int>> circleId, List<List<double>> circleX, List<List<double>> circleY, List<List<double>> circleRadius)
    {
        
        for (int i = 0; i < circleId.Count; i++)
        {
            for (int j = 0; j < circleId[i].Count; j++)
            {
                UpdateCircleValues(circleId[i][j], circleX[i][j], circleY[i][j], circleRadius[i][j], (playerId[i] == currentPlayerId));
            }
        }
    }

    public void UpdateCircleValues(int circleId, double circleX, double circleY, double circleRadius, bool belongsToCurrentPlayer)
    {
        SortedSet<CircleController> searchList =
            (belongsToCurrentPlayer) ? currentPlayerCircles : otherPlayersCircles;

        foreach (CircleController circle in searchList)
        {
            if (circle.Id == circleId)
            {
                circle.CircleFrameUpdate((float)circleX, (float)circleY, (float)circleRadius);
                return;
            }
        }
    }

    public void AddCircles(int playerId, List<int> circleId, List<double> circleX, List<double> circleY, List<double> circleRadius)
    {
        for (int i = 0; i < circleId.Count; i++)
        {
            AddCircle(circleId[i], circleX[i], circleY[i], circleRadius[i], (playerId == currentPlayerId));
        }
    }

    public void AddCircle(int circleId, double circleX, double circleY, double circleRadius, bool belongsToCurrentPlayer)
    {
        CircleController newCircle = Instantiate(circlePrefab);
        newCircle.Id = circleId;
        newCircle.transform.position = new Vector2((float)circleX, (float)circleY);
        newCircle.transform.localScale = new Vector3((float)circleRadius, (float)circleRadius, (float)circleRadius);

        if (belongsToCurrentPlayer)
        {
            newCircle.transform.SetParent(currentPlayerContainer.transform);
            currentPlayerCircles.Add(newCircle);
        } else
        {
            newCircle.transform.SetParent(otherPlayersContainer.transform);
            otherPlayersCircles.Add(newCircle);
        }
    }

    public void RemoveCircles(List<int> playerId, List<int> circleId)
    {
        for (int i = 0; i < playerId.Count; i++)
        {
            RemoveCircle(circleId[i], (playerId[i] == currentPlayerId));
        }

    }

    public void RemoveCircle(int circleId, bool belongsToCurrentPlayer)
    {
        SortedSet<CircleController> searchList =
            (belongsToCurrentPlayer) ? currentPlayerCircles : otherPlayersCircles;

        foreach (CircleController circle in searchList)
        {
            if (circle.Id == circleId)
            {
                searchList.Remove(circle);
                circle.KillCircle();
                return;
            }
        }
    }

    public void KillCurrentPlayer()
    {
        foreach (CircleController circle in currentPlayerCircles)
        {
            currentPlayerCircles.Remove(circle);
            circle.KillCircle();
        }
    }

    public void KillEveryone()
    {
        KillCurrentPlayer();

        foreach (CircleController circle in otherPlayersCircles)
        {
            currentPlayerCircles.Remove(circle);
            circle.KillCircle();
        }

    }
}
