using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Server
{
    public class Player : IComparable<Player>
    {
        private const int SPLIT_TIMER_TIME_MS = 40000;
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

        public event EventHandler<PlayerDiedEventArgs> OnPlayerDied;
        public event EventHandler<PlayerCirclesAddedEventArgs> OnPlayerCirclesAdded;
        public event EventHandler<PlayerCirclesRemovedEventArgs> OnPlayerCirclesRemoved;

        public Player(int playerId)
        {
            Id = playerId;
            PlayerCircles = new List<Circle>();
            IsPresent = true;
            velocityX = 0;
            velocityY = 0;
        }

        public void StartNewGame(Point firstCirclePoint)
        {
            Score = 0;
            PlayerCircles.Clear();
            AddCircle(new Circle(firstCirclePoint));
            Score += Math.Pow(PlayerCircles[0].Radius, 2);
        }

        public void Split()
        {
            double minSplitRadius = 0.02;//Circle.MIN_RADIUS * 1.414;
            List<Circle> newCircles = new List<Circle>();

            foreach (Circle circle in PlayerCircles)
            {
                if (circle.Radius >= minSplitRadius) 
                {
                    circle.Radius /= 2;
                    Circle newCircle = new Circle(circle.Position, circle.Radius, true);
                    newCircles.Add(newCircle);
                }
            }

            Thread splitTimer = new Thread(StartSplitTimer);
            splitTimer.Start(newCircles);

            OnPlayerCirclesAdded?.Invoke(this, new PlayerCirclesAddedEventArgs(newCircles, this.Id));

            foreach(Circle circle in newCircles)
            {
                AddCircle(circle);
            }
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
                circle.Move(velocityX, velocityY);
            }

            //UpdateBorderlineCoordinates();
        }

        public void Move(double leftBorder, double rightBorder, double topBorder, double bottomBorder)
        {
            foreach (Circle circle in PlayerCircles)
            {
                circle.Move(velocityX, velocityY, leftBorder, rightBorder, topBorder, bottomBorder);
            }

            //UpdateBorderlineCoordinates();
        }

        public List<EatPair<Circle>> CalculateCirclesEatPairs(Player other)
        {
            List<EatPair<Circle>> eatPairs = new List<EatPair<Circle>>();

            for (int i = 0; i < this.PlayerCircles.Count; i++)
            {
                for (int j = 0; j < other.PlayerCircles.Count; j++)
                {
                    if (this.PlayerCircles[i].CanEatOtherObject(other.PlayerCircles[j]))
                    {
                        eatPairs.Add(new EatPair<Circle>(this.PlayerCircles[i], other.PlayerCircles[j]));
                    }
                }
            }

            return eatPairs;
        }

        public List<EatPair<Food>> CalculateFoodEatPairs(Food food)
        {
            List<EatPair<Food>> eatPairs = new List<EatPair<Food>>();

            for (int i = 0; i < this.PlayerCircles.Count; i++)
            {
                if (this.PlayerCircles[i].CanEatOtherObject(food))
                {
                    eatPairs.Add(new EatPair<Food>(this.PlayerCircles[i], food));
                }
            }

            return eatPairs;
        }

        private void AddCircle(Circle circle)
        {
            circle.OnCircleEaten += OnCircleEaten;
            circle.OnCircleAte += OnCircleAte;
            PlayerCircles.Add(circle);
        }

        private void StartSplitTimer(object newCirclesObj)
        {
            if (newCirclesObj is List<Circle> newCircles)
            {
                Thread.Sleep(SPLIT_TIMER_TIME_MS);

                if (PlayerCircles.Count == 0)
                {
                    return;
                }

                foreach (Circle circle in newCircles)
                {
                    PlayerCircles[0].EatObject(circle);
                }

                OnPlayerCirclesRemoved?.Invoke(this, new PlayerCirclesRemovedEventArgs(newCircles, Id));
            }
        }

        private void UpdateBorderlineCoordinates()
        {
            double leftX = 0;
            double rightX = 0;
            double bottomY = 0;
            double topY = 0;

            foreach (Circle circle in PlayerCircles)
            {
                leftX = Math.Min(circle.Position.X, leftX);
                rightX = Math.Max(circle.Position.X, rightX);
                bottomY = Math.Min(circle.Position.Y, bottomY);
                topY = Math.Max(circle.Position.Y, topY);
            }

            LeftX = leftX;
            RightX = rightX;
            BottomY = bottomY;
            TopY = topY;
        }

        private void OnCircleEaten(object sender, CircleEatenEventArgs eventArgs)
        {
            Score -= Math.Pow(eventArgs.Eaten.Radius, 2);
            if (PlayerCircles.Count == 0)
            {
                OnPlayerDied?.Invoke(this, new PlayerDiedEventArgs(this));
            }
        }
        private void OnCircleAte(object sender, CircleAteEventArgs eventArgs)
        {
            Score += Math.Pow(eventArgs.Eater.Radius, 2);
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
