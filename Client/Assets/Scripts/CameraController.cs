using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void FollowCenter()
    {
        Vector3 pos = Vector3.zero;
        foreach (CircleController circle in PlayerManager.Instance.currentPlayerCircles)
        {
            pos += circle.gameObject.transform.position;
        }
        transform.position = pos / PlayerManager.Instance.currentPlayerCircles.Count;
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
