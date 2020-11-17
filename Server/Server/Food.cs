namespace Server
{
    public class Food : EatableObject
    {
        private const double FOOD_RADIUS = 0.005;

        public Food(Point position) : base(position)
        {
            Radius = FOOD_RADIUS;
        }
    }
}
