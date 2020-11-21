﻿using System.Collections.Generic;

namespace Server
{
    public class Cell
    {
        public const double HEIGHT = 100;
        public const double WIDTH = 100;
        private SortedSet<Circle> containedCircles;
        private SortedSet<Food> containedFood;
        private double leftX;
        private double rightX;
        private double bottomY;
        private double topY;
        public int CellX { get; private set; }
        public int CellY { get; private set; }

        public Cell(int cellX, int cellY)
        {
            CellX = cellX;
            CellY = cellY;

            leftX = cellX * WIDTH;
            rightX = leftX + WIDTH;
            bottomY = cellY * HEIGHT;
            topY = topY + HEIGHT;

            containedCircles = new SortedSet<Circle>();
            containedFood = new SortedSet<Food>();
        }

        public void AddCircle(Circle circle)
        {
            containedCircles.Add(circle);
            circle.OnCircleEaten += (s, ea) => RemoveCircle(ea.Eaten);
        }

        public void AddFood(Food food)
        {
            containedFood.Add(food);
            food.OnFoodEaten += (s, ea) => RemoveFood(ea.Eaten);
        }

        public void Update()
        {

        }

        public bool EatableObjectIsIn(EatableObject eatableObject)
        {
            double eatableObjectLeftX = eatableObject.Position.X - eatableObject.Radius;
            double eatableObjectRightX = eatableObject.Position.X + eatableObject.Radius;
            double eatableObjectBottomY = eatableObject.Position.Y - eatableObject.Radius;
            double eatableObjectTopY = eatableObject.Position.Y + eatableObject.Radius;

            bool horizontallyInside = (leftX < eatableObjectLeftX) && (eatableObjectRightX < rightX);
            bool verticallyInside = (bottomY < eatableObjectBottomY) && (eatableObjectTopY < topY);

            return horizontallyInside && verticallyInside;
        }

        private void RemoveCircle(Circle circle)
        {
            containedCircles.Remove(circle);
        }

        private void RemoveFood(Food food)
        {
            containedFood.Remove(food);
        }
    }
}
