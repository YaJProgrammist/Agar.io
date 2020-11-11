using System.Collections.Generic;

namespace Server
{
    public class Circle : EatableObject
    {
        public const double MIN_RADIUS = 2;
        private List<Circle> childCircles;

        public bool IsRemoved { get; private set; }

        public Circle(double x, double y, double radius = MIN_RADIUS) : base (x, y)
        {
            childCircles = new List<Circle>();
            Radius = radius;
            IsRemoved = false;
        }

        public void Move(double velocityX, double velocityY)
        {
            double speed = 10 / Radius;
            X += velocityX * speed;
            Y += velocityY * speed;
        }

        public override void Remove()
        {
            IsRemoved = true;
            base.Remove();
        }
    }
}
