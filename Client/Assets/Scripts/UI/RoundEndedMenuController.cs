using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEndedMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject roundEndedMenu;

    [SerializeField]
    GameObject ratingStringPrefab;

    public void UpdateRating(List<Tuple<int, int>> rating)
    {

    }

    public void Open()
    {
        roundEndedMenu.SetActive(true);
    }

    public void Close()
    {
        roundEndedMenu.SetActive(false);
    }
}
