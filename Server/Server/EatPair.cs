namespace Server
{
    public struct EatPair<T> where T : EatableObject
    {
        public Circle Eater { get; private set; }
        public T Eaten { get; private set; }

        public EatPair(Circle eater, T eaten)
        {
            Eater = eater;
            Eaten = eaten;
        }
    }
}
