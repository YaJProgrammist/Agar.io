using System;
using System.Collections.Generic;

namespace Server
{
    public abstract class EatableObject
    {
        private List<Circle> childCircles;
        public int Id { get; private set; }
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public double Radius { get; set; }

        public EatableObject(double x, double y)
        {
            X = x;
            Y = y;
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
