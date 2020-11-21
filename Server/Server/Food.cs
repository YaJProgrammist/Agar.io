using System;

namespace Server
{
    public class Food : EatableObject
    {
        private const double FOOD_RADIUS = 0.05;

        public event EventHandler<FoodEatenEventArgs> OnFoodEaten;

        public Food(Point position) : base(position)
        {
            Radius = FOOD_RADIUS;
        }

        public override void Remove()
        {
            OnFoodEaten?.Invoke(this, new FoodEatenEventArgs(this));
            base.Remove();
        }
    }
}
