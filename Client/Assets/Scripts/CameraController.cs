using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private SortedSet<Transform> circles;

    public void AddCircle(Transform circle)
    {
        circles.Add(circle);
    }

    public void RemoveCircle(Transform circle)
    {
        circles.Remove(circle);
    }

    private Vector3 FindCenter()
    {
        Vector3 pos = Vector3.zero;
        foreach (Transform t in circles)
        {
            pos += t.position;
        }
        return pos / circles.Count;
    }

    private void FixedUpdate()
    {
        transform.position = FindCenter();
    }

    private void Awake()
    {
        circles = new SortedSet<Transform>();
    }
}
