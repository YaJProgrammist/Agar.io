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

    private List<RatingSectorController> createdSectors;

    private void Awake()
    {
        createdSectors = new List<RatingSectorController>();
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

    public void UpdateRating(List<Tuple<string, int>> rating)
    {
        // create new sectors and set grid as parent
        // foreach new sector make sector.UpdateValues(playername, score)
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
