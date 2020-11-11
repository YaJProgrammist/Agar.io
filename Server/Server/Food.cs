namespace Server
{
    public class Food : EatableObject
    {
        private const double FOOD_RADIUS = 0.5;

        public Food(int x, int y) : base(x, y)
        {
            X = x;
            Y = y;
            Radius = FOOD_RADIUS;
        }
    }
}
