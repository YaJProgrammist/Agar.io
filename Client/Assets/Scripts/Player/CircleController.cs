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
        StartCoroutine(MoveAndChangeSize(newX, newY, newRadius, 0.02f));
    }

    private IEnumerator MoveAndChangeSize(float newX, float newY, float newRadius, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(newX, newY), (elapsedTime / time));
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(newRadius, newRadius), (elapsedTime / time));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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
