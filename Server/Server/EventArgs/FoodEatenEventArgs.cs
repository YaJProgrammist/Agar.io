namespace Server
{
    public class FoodEatenEventArgs
    {
        public Food Eaten { get; private set; }

        public FoodEatenEventArgs(Food eaten)
        {
            Eaten = eaten;
        }
    }
}
