using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void FollowCenter()
    {
        if (PlayerManager.Instance.currentPlayerCircles.Count != 0)
        {
            Vector3 pos = new Vector3(0, 0, transform.position.z);
            foreach (CircleController circle in PlayerManager.Instance.currentPlayerCircles)
            {
                pos += circle.gameObject.transform.position;
            }
            transform.position = pos / PlayerManager.Instance.currentPlayerCircles.Count;
        }
    }

    private void ChangeResolution()
    {

    }

    private void FixedUpdate()
    {
        FollowCenter();
        ChangeResolution();

    }
}
