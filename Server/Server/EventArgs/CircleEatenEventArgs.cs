namespace Server
{
    public class CircleEatenEventArgs
    {
        public Circle Eaten { get; private set; }

        public CircleEatenEventArgs(Circle eaten)
        {
            Eaten = eaten;
        }
    }
}
