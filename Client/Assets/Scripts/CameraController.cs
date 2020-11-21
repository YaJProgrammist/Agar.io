using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float resolutionCoefficient = 35.7f;

    [SerializeField]
    float timeBetweenMoving = 0.02f;

    private Camera currentPlayerCamera;

    //added to avoid camera's long moving to player on the start of a round
    private bool hasFoundObject = false;

    private Vector2 zeroPoint = new Vector2(500, 500);
    private float zeroScale = 7.14f;

    public void ResetCameraView()
    {
        transform.position = zeroPoint;
        currentPlayerCamera.orthographicSize = zeroScale;
    }

    private void FollowCenterAndChangeResolution()
    {
        if (PlayerManager.Instance.currentPlayerCircles.Count != 0)
        {
            // when round starts the camera need to move from zero point to the player's position
            if (!hasFoundObject)
            {
                //as on start each player has only one circle move camera to our the only existed circle
                transform.position = PlayerManager.Instance.currentPlayerCircles[0].transform.position;
                hasFoundObject = true;

            } 
                Vector3 pos = new Vector3(0, 0, transform.position.z);
                float circlesRadiusSum = 0;

                foreach (CircleController circle in PlayerManager.Instance.currentPlayerCircles)
                {
                    pos.x += circle.gameObject.transform.position.x;
                    pos.y += circle.gameObject.transform.position.y;

                    circlesRadiusSum += circle.gameObject.transform.localScale.x;
                }
                // move with circles
                transform.position = Vector3.Lerp(transform.position, pos / PlayerManager.Instance.currentPlayerCircles.Count, timeBetweenMoving);

                // change resolution
                currentPlayerCamera.orthographicSize = Mathf.Lerp(currentPlayerCamera.orthographicSize, circlesRadiusSum * resolutionCoefficient, timeBetweenMoving);
        }
    }

    private void FixedUpdate()
    {
        FollowCenterAndChangeResolution();
    }

    private void Awake()
    {
        currentPlayerCamera = GetComponent<Camera>();
    }
}
