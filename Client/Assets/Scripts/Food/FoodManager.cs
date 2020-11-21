using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance { get; private set; }

    [SerializeField]
    FoodParticleController foodPrefab;

    [SerializeField]
    GameObject foodParent;

    [SerializeField]
    Color[] colors;

    private List<FoodParticleController> existedFood;

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

    public void SpawnFood(List<int> foodId, List<double> foodX, List<double> foodY, List<double> foodRadius)
    {
        for (int i = 0; i < foodId.Count; i++)
        {
            SpawnFoodItem(foodId[i], foodX[i], foodY[i], foodRadius[i]);
        }
    }

    public void SpawnFoodItem(int foodId, double x, double y, double radius)
    {
        FoodParticleController newFoodItem = Instantiate(foodPrefab);
        newFoodItem.Id = foodId;

        newFoodItem.name = "food" + newFoodItem.Id;
        newFoodItem.transform.position = new Vector2((float)x, (float)y);
        newFoodItem.transform.SetParent(foodParent.transform);
        newFoodItem.transform.localScale = new Vector3((float)radius, (float)radius, (float)radius);

        newFoodItem.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];

        existedFood.Add(newFoodItem);
    }

    public void RemoveFood(List<int> foodId)
    {
        for (int i = 0; i < foodId.Count; i++)
        {
            RemoveFoodItem(foodId[i]);
        }
    }

    public void RemoveFoodItem(int foodId)
    {
        for(int i = 0; i < existedFood.Count; i++)
        {
            if (existedFood[i].Id == foodId)
            {
                Debug.LogWarning("found");
                existedFood[i].SwallowFoodParticle();
                existedFood.RemoveAt(i);
                i--;
            }
        }
    }

}
