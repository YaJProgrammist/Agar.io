using System;
using System.Threading;

namespace Server
{
    public class Circle : EatableObject
    {
        public const double MIN_RADIUS = 20;// 0.2;
        public const double NORMAL_SPEED_COEFF = 0.1;
        public const double ACCELERATED_SPEED_COEFF = 0.2;
        public const int ACCELERATION_TIME_MS = 1000;
        private double speedCoeff;
        public int LeftCellX { get; private set; }
        public int RightCellX { get; private set; }
        public int TopCellY { get; private set; }
        public int BottomCellY { get; private set; }

        public bool IsRemoved { get; private set; }
        public bool IsAccelerated { get; private set; }

        public event EventHandler<CircleEatenEventArgs> OnCircleEaten;
        public event EventHandler<CircleAteEventArgs> OnCircleAte;

        public Circle(Point position, double radius = MIN_RADIUS, bool isAccelerated = false) : base (position)
        {
            Radius = radius;
            IsRemoved = false;

            IsAccelerated = isAccelerated;

            if (IsAccelerated)
            {
                speedCoeff = ACCELERATED_SPEED_COEFF;
                Thread accelerationTimerThread = new Thread(StartAccelerationTimer);
                accelerationTimerThread.Start();
            }
            else
            {
                speedCoeff = NORMAL_SPEED_COEFF;
            }

            UpdateBorderlineCells();
        }

        public void Move(double velocityX, double velocityY)
        {
            double speed = speedCoeff / Radius;

            Position = new Point(Position.X + velocityX * speed, Position.Y + velocityY * speed);

            UpdateBorderlineCells();
        }

        public void Move(double velocityX, double velocityY, double leftBorder, double rightBorder, double topBorder, double bottomBorder)
        {
            double speed = speedCoeff / Radius;

            double newPositionX = Math.Max(Math.Min(Position.X + velocityX * speed, rightBorder), leftBorder);
            double newPositionY = Math.Max(Math.Min(Position.Y + velocityY * speed, topBorder), bottomBorder);
            Position = new Point(newPositionX, newPositionY);

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

            double otherLeftX = other.Position.X - other.Radius;
            double otherRightX = other.Position.X + other.Radius;
            double otherBottomY = other.Position.Y - other.Radius;
            double otherTopY = other.Position.Y + other.Radius;

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

        private void StartAccelerationTimer()
        {
            Thread.Sleep(ACCELERATION_TIME_MS);
            speedCoeff = NORMAL_SPEED_COEFF;
            IsAccelerated = false;
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
