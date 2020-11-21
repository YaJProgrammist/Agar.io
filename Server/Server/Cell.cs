using System.Collections.Generic;

namespace Server
{
    public class Cell
    {
        public const double HEIGHT = 100;
        public const double WIDTH = 100;
        private SortedSet<Circle> containedCircles;
        private SortedSet<Food> containedFood;

        public Cell()
        {
            containedCircles = new SortedSet<Circle>();
            containedFood = new SortedSet<Food>();
        }

        public void AddCircle(Circle circle)
        {
            containedCircles.Add(circle);
            circle.OnCircleEaten += (s, ea) => RemoveCircle(ea.Eaten);
        }

        public void AddFood(Food food)
        {
            containedFood.Add(food);
            food.OnFoodEaten += (s, ea) => RemoveFood(ea.Eaten);
        }

        public void Update()
        {

        }

        /*public bool EatableObjectIsIn(EatableObject eatableObject)
        {

        }

        public bool EatableObjectIsIn(EatableObject eatableObject)
        {

        }*/

        private void RemoveCircle(Circle circle)
        {
            containedCircles.Remove(circle);
        }

        private void RemoveFood(Food food)
        {
            containedFood.Remove(food);
        }
    }
}
