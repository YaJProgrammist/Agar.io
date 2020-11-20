namespace Server
{
    public class CircleAteEventArgs
    {
        public Circle Eater { get; private set; }

        public CircleAteEventArgs(Circle eater)
        {
            Eater = eater;
        }
    }
}
