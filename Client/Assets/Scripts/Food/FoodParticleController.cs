using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodParticleController : MonoBehaviour
{
    public int Id;

    public void SwallowFoodParticle()
    {
        Destroy(gameObject);
    }

}
