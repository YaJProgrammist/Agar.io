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
        transform.position = new Vector2(newX, newY);
        //transform.position = Vector2.Lerp(transform.position, new Vector2(newX, newY), 0.2f);
        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(newRadius, newRadius), 0.2f);
    }

    public void SetPlayerStartValues(int Id, float x, float y, float radius, Color color)
    {
        transform.position = new Vector3(x, y);
        transform.localScale = new Vector3(radius, radius);
        GetComponent<SpriteRenderer>().color = color;
      
    }

    public void KillCircle()
    {
        Destroy(gameObject);
    }

    
}
