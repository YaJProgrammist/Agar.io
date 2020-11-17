using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance { get; private set; }

    [SerializeField]
    GameObject foodPrefab;

    [SerializeField]
    GameObject foodParent;

    [SerializeField]
    Color[] colors;

    private List<GameObject> existedFood;

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

    private void Awake()
    {
        CreateSingleton();
    }

    public void SpawnOneFoodItem(float x, float y, float radius)
    {
        GameObject newFoodItem = Instantiate(foodPrefab);
        newFoodItem.transform.position = new Vector2(x, y);
        newFoodItem.transform.SetParent(foodParent.transform);
        newFoodItem.transform.localScale = new Vector3(radius, radius, radius);
        newFoodItem.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
    }

    //rewrite using sorted ID instead of comparing positions
    public void RemoveOneFoodItem(float x, float y)
    {
        for(int i = 0; i < existedFood.Count; i++)
        {
            if (existedFood[i].transform.position.x == x && existedFood[i].transform.position.y == y)
            {
                Destroy(existedFood[i]);
                existedFood.RemoveAt(i);
            }
        }
    }

    //List<> food
    public void SpawnFood()
    {

    }

    //List<> food
    public void RemoveFood()
    {

    }
}
