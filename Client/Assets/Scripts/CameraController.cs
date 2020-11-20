using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 FindCenter()
    {
        Vector3 pos = Vector3.zero;
        foreach (CircleController circle in PlayerManager.Instance.currentPlayerCircles)
        {
            pos += circle.gameObject.transform.position;
        }
        return pos / PlayerManager.Instance.currentPlayerCircles.Count;
    }

    private void FixedUpdate()
    {
        transform.position = FindCenter();
    }
}
