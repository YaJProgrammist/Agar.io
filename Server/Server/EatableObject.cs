using System;

namespace Server
{
    public abstract class EatableObject
    {
        public int Id { get; private set; }
        public Point Position { get; protected set; }
        public double Radius { get; set; }

        public EatableObject(Point position)
        {
            Position = position;
        }

        public void SwallowEatableObject(EatableObject swallowed)
        {
            this.Radius = Math.Sqrt(Math.Pow(this.Radius, 2) + Math.Pow(swallowed.Radius, 2));
            swallowed.Remove();
        }

        public virtual void Remove()
        {
            //EventsSender.GetInstance().RegisterEvent(); // TODO
        }
    }
}
