using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
    public int Id;

    //each 0.2 sec
    //private void FixedUpdate()
    //{
    //    if (isMoving)
    //    {
    //        //UpdatePosition(newPosition);
    //    }
    //}

    public void CircleFrameUpdate(float newX, float newY, float newRadius)
    {
        transform.position = new Vector3(newX, newY, 0);
        transform.localScale = new Vector3(newRadius, newRadius, newRadius);
    }

    public void SetPlayerStartValues(int Id, float x, float y, float radius, Color color)
    {
        transform.position = new Vector3(x, y, 0);
        transform.localScale = new Vector3(radius, radius, radius);
        GetComponent<SpriteRenderer>().color = color;
      
    }

    public void KillCircle()
    {
        Destroy(gameObject);
    }

    
}
