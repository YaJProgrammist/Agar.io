using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Server
{
    public class Player : IComparable<Player>
    {
        private double velocityX;
        private double velocityY;

        public int Id { get; set; }
        public double Score { get; set; }
        public List<Circle> PlayerCircles { get; private set; }
        public bool IsPresent { get; private set; }
        public double LeftX { get; private set; }
        public double RightX { get; private set; }
        public double TopY { get; private set; }
        public double BottomY { get; private set; }

        public Player()
        {
            PlayerCircles = new List<Circle>();
            IsPresent = true;
        }

        public void StartNewGame(double firstCircleX, double firstCircleY)
        {
            Score = 0;
            PlayerCircles.Clear();
            PlayerCircles.Add(new Circle(firstCircleX, firstCircleY));
        }

        public void Split()
        {
            double minSplitRadius = Circle.MIN_RADIUS * 2;
            List<Circle> newCircles = new List<Circle>();

            foreach (Circle circle in PlayerCircles)
            {
                if (circle.Radius >= minSplitRadius)
                {
                    circle.Radius /= 2;
                    newCircles.Add(new Circle(circle.X, circle.Y, circle.Radius));
                }
            }

            PlayerCircles.AddRange(newCircles);
        }

        public void SetVelocity(double newVelocityX, double newVelocityY)
        {
            velocityX = newVelocityX;
            velocityY = newVelocityY;
        }

        public void Move()
        {
            foreach (Circle circle in PlayerCircles)
            {
                if (circle.IsRemoved)
                {
                    Score -= circle.Radius;
                    continue;
                }

                circle.Move(velocityX, velocityY);
            }

            //UpdateBorderlineCoordinates();
        }

        public List<Circle> TryEatPlayer(Player other)
        {
            List<Circle> eatenCircles = new List<Circle>();

            for (int i = 0; i < this.PlayerCircles.Count; i++)
            {
                for (int j = 0; j < other.PlayerCircles.Count; j++)
                {
                    if (this.PlayerCircles[i].CanEatOtherObject(other.PlayerCircles[j]))
                    {
                        eatenCircles.Add(other.PlayerCircles[j]);

                    }
                }
            }

            return eatenCircles;
        }

        private void UpdateBorderlineCoordinates()
        {
            double leftX = 0;
            double rightX = 0;
            double bottomY = 0;
            double topY = 0;

            foreach (Circle circle in PlayerCircles)
            {
                leftX = Math.Min(circle.X, leftX);
                rightX = Math.Max(circle.X, rightX);
                bottomY = Math.Min(circle.Y, bottomY);
                topY = Math.Max(circle.Y, topY);
            }

            LeftX = leftX;
            RightX = rightX;
            BottomY = bottomY;
            TopY = topY;
        }

        private double GetCameraHeight()
        {
            return Math.Max(RightX - LeftX, TopY - BottomY);
        }

        public int CompareTo([AllowNull] Player other)
        {
            if (other == null || this.Score > other.Score)
            {
                return 1;
            }

            if (this.Score == other.Score)
            {
                return 0;
            }

            return -1;
        }
    }
}
