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
                pos.x += circle.gameObject.transform.position.x;
                pos.y += circle.gameObject.transform.position.y;
            }
            transform.position = Vector3.Lerp(transform.position, pos / PlayerManager.Instance.currentPlayerCircles.Count, 0.02f);
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
