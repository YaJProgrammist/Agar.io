using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndedMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject roundEndedMenu;

    [SerializeField]
    RatingSectorController ratingSectorPrefab;

    [SerializeField]
    GameObject grid;

    [SerializeField]
    Button restartButton;

    [SerializeField]
    Canvas canvas;

    private List<RatingSectorController> createdSectors;

    private void Awake()
    {
        createdSectors = new List<RatingSectorController>();

        restartButton.onClick.AddListener(()=> {
            GameManager.Instance.StartGame();
            EventsSender.RegisterEvent(new LeaveGame(PlayerManager.Instance.currentPlayerId));
        });

    }

    private void DeleteCurrentRating()
    {
        int counter = 0;
        for (int i = 0; i < createdSectors.Count; i++)
        {
            Destroy(createdSectors[counter].gameObject);
            createdSectors.RemoveAt(counter);
            ++counter;
        }
    }

    public void UpdateRating(List<int> playersId, List<double> playerScore)
    {
        float scaleHeight = ratingSectorPrefab.GetComponent<RectTransform>().rect.height * canvas.scaleFactor;
        float scaleWidth = ratingSectorPrefab.GetComponent<RectTransform>().rect.width * canvas.scaleFactor;

        for (int i = 0; i < playersId.Count; i++)
        {
            RatingSectorController newSector = Instantiate(ratingSectorPrefab);
            newSector.GetComponent<RectTransform>().sizeDelta = new Vector2(scaleWidth, scaleHeight);
            newSector.transform.SetParent(grid.transform);

            newSector.UpdateValues(playersId[i], playerScore[i]);

            createdSectors.Add(newSector);
        }
    }

    public void Open()
    {
        roundEndedMenu.SetActive(true);
    }

    public void Close()
    {
        DeleteCurrentRating();
        roundEndedMenu.SetActive(false);
    }
}
