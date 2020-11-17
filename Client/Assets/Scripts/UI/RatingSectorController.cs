using UnityEngine;
using UnityEngine.UI;

public class RatingSectorController : MonoBehaviour
{
    [SerializeField]
    Text playerName;

    [SerializeField]
    Text score;

    public void UpdateValues(string name, int newScore)
    {
        playerName.text = name;
        score.text = newScore.ToString();
    }
}
