using UnityEngine;
using UnityEngine.UI;

public class RatingSectorController : MonoBehaviour
{
    [SerializeField]
    Text playerId;

    [SerializeField]
    Text score;

    public void UpdateValues(int id, double newScore)
    {
        playerId.text = id.ToString();
        score.text = newScore.ToString();
    }
}
