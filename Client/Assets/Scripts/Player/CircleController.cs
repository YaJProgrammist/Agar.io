using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
    [SerializeField]
    Text Name;

    [SerializeField]
    float timeBetweenMoving = 0.02f;

    public int Id;
    public bool isMoving = false;

    private Vector2 lastSavedPosition;
    private Vector2 lastSavedScale;

    private Vector2 newPosition;
    private Vector2 newScale;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveAndChangeSize();
        }
    }

    public void SetName(string name, bool isCurrentPlayer = false)
    {
        Name.text = (isCurrentPlayer)? PlayerManager.Instance.currentPlayerName : name;
    }

    public void CircleFrameUpdate(float newX, float newY, float newRadius)
    {
        newPosition = new Vector2(newX, newY);
        newScale = new Vector2(newRadius, newRadius);
    }

    private void MoveAndChangeSize()
    {
       transform.position = Vector2.Lerp(transform.position, newPosition, timeBetweenMoving);
       transform.localScale = Vector2.Lerp(transform.localScale, newScale, timeBetweenMoving);
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
