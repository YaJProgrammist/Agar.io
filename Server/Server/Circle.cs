using System.Collections.Generic;

namespace Server
{
    public class Circle : EatableObject
    {
        public const double MIN_RADIUS = 2;
        private List<Circle> childCircles;
        public int LeftCellX { get; private set; }
        public int RightCellX { get; private set; }
        public int TopCellY { get; private set; }
        public int BottomCellY { get; private set; }

        public bool IsRemoved { get; private set; }

        public Circle(double x, double y, double radius = MIN_RADIUS) : base (x, y)
        {
            childCircles = new List<Circle>();
            Radius = radius;
            IsRemoved = false;

            UpdateBorderlineCells();
        }

        public void Move(double velocityX, double velocityY)
        {
            double speed = 10 / Radius;
            X += velocityX * speed;
            Y += velocityY * speed;

            UpdateBorderlineCells();
        }

        public override void Remove()
        {
            IsRemoved = true;
            base.Remove();
        }

        public bool CanEatOtherObject(EatableObject other)
        {
            if (this.Radius <= other.Radius)
            {
                return false;
            }

            double otherLeftX = other.X - Radius;
            double otherRightX = other.X + Radius;
            double otherBottomY = other.Y - Radius;
            double otherTopY = other.Y + Radius;

            bool horizontallyInside = (this.X - Radius < otherLeftX) && (otherRightX < this.X + Radius);
            bool verticallyInside = (this.Y - Radius < otherBottomY) && (otherTopY < this.Y + Radius);

            return horizontallyInside && verticallyInside;
        }

        private void UpdateBorderlineCells()
        {
            double leftX = X - Radius;
            LeftCellX = (int)(leftX / Cell.WIDTH);

            double rightX = X + Radius;
            RightCellX = (int)(rightX / Cell.WIDTH);

            double bottomY = Y - Radius;
            BottomCellY = (int)(bottomY / Cell.HEIGHT);

            double topY = Y + Radius;
            TopCellY = (int)(topY / Cell.HEIGHT);
        }
    }
}
