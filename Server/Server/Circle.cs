using System;
using System.Collections.Generic;

namespace Server
{
    public class Circle : EatableObject
    {
        public const double MIN_RADIUS = 0.02;
        public const double NORMAL_SPEED_COEFF = 1;
        public const double ACCELERATED_SPEED_COEFF = 2;
        private List<Circle> childCircles;
        private double speedCoeff;
        public int LeftCellX { get; private set; }
        public int RightCellX { get; private set; }
        public int TopCellY { get; private set; }
        public int BottomCellY { get; private set; }

        public bool IsRemoved { get; private set; }
        public bool IsAccelerated { get; private set; }

        public Circle(Point position, double radius = MIN_RADIUS, bool isAccelerated = false) : base (position)
        {
            childCircles = new List<Circle>();
            Radius = radius;
            IsRemoved = false;

            IsAccelerated = isAccelerated;

            if (IsAccelerated)
            {
                speedCoeff = ACCELERATED_SPEED_COEFF;
            }
            else
            {
                speedCoeff = NORMAL_SPEED_COEFF;
            }

            UpdateBorderlineCells();
        }

        public void Move(double velocityX, double velocityY)
        {
            double speed = 1 / Radius;
            Position = new Point(Position.X + velocityX * speed, Position.Y + velocityY * speed);

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

            double otherLeftX = other.Position.X - Radius;
            double otherRightX = other.Position.X + Radius;
            double otherBottomY = other.Position.Y - Radius;
            double otherTopY = other.Position.Y + Radius;

            bool horizontallyInside = (this.Position.X - Radius < otherLeftX) && (otherRightX < this.Position.X + Radius);
            bool verticallyInside = (this.Position.Y - Radius < otherBottomY) && (otherTopY < this.Position.Y + Radius);

            return horizontallyInside && verticallyInside;
        }

        public void EatObject(EatableObject other)
        {
            if (other == null)
            {
                return;
            }

            this.Radius = Math.Sqrt(Math.Pow(this.Radius, 2) + Math.Pow(other.Radius, 2));

            other.Remove();
        }

        private void UpdateBorderlineCells()
        {
            double leftX = Position.X - Radius;
            LeftCellX = (int)(leftX / Cell.WIDTH);

            double rightX = Position.X + Radius;
            RightCellX = (int)(rightX / Cell.WIDTH);

            double bottomY = Position.Y - Radius;
            BottomCellY = (int)(bottomY / Cell.HEIGHT);

            double topY = Position.Y + Radius;
            TopCellY = (int)(topY / Cell.HEIGHT);
        }
    }
}
