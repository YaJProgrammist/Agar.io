using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CircleManager : MonoBehaviour
{
    public static CircleManager Instance { get; private set; }
    public bool isMoving = false;

    private string name;
    private Rigidbody2D rb;
    private int id = 0;

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    private void InitializeManager()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }

    //each 0.2 sec
    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;
            // send direction to server

            // get new position from server
            //Vector2 newPosition = ;
            //UpdatePosition(newPosition);
        }
    }

    public void UpdateRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }

    public void UpdatePosition(Vector2 newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }

    public void SetPlayerStartValues(int id, float x, float y, float radius, Color color)
    {
        transform.position = new Vector3(x, y, 0);
        transform.localScale = new Vector3(radius, radius, radius);
        GetComponent<SpriteRenderer>().color = color;
      
    }

    public void SetPlayerName(string nickname)
    {
        name = nickname;
    }

    
}
