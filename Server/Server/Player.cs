using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class Player
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public List<Circle> playerCircles { get; private set; }
        public bool IsPresent { get; private set; }

        public Player()
        {
            playerCircles = new List<Circle>();
            IsPresent = true;
        }

        public void StartNewGame(double firstCircleX, double firstCircleY)
        {
            Score = 0;
            playerCircles.Clear();
            playerCircles.Add(new Circle(firstCircleX, firstCircleY));
        }

        public void Split()
        {
            double minSplitRadius = Circle.MIN_RADIUS * 2;
            List<Circle> newCircles = new List<Circle>();

            foreach (Circle circle in playerCircles)
            {
                if (circle.Radius >= minSplitRadius)
                {
                    circle.Radius /= 2;
                    newCircles.Add(new Circle(circle.X, circle.Y, circle.Radius));
                }
            }

            playerCircles.AddRange(newCircles);
        }

        public void Move(double velocityX, double velocityY)
        {
            foreach (Circle circle in playerCircles)
            {
                if (circle.IsRemoved)
                {
                    Score -= circle.Radius;
                    continue;
                }

                circle.Move(velocityX, velocityY);
            }           
        }
    }
}
