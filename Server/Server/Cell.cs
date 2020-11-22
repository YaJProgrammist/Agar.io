using System;
using System.Collections.Generic;

namespace Server
{
    public class Cell
    {
        public const double HEIGHT = 100;
        public const double WIDTH = 100;

        private double leftX;
        private double rightX;
        private double bottomY;
        private double topY;
        private int id;

        public int CellX { get; private set; }
        public int CellY { get; private set; }
        public List<Player> ContainedPlayers { get; private set; }
        public List<Food> ContainedFood { get; private set; }
        public List<Cell> SurroundingCells { get; set; }

        public event EventHandler<PlayerAddedEventArgs> OnPlayerAdded;
        public event EventHandler<PlayerLeftEventArgs> OnPlayerLeft;

        public Cell(int cellX, int cellY)
        {
            CellX = cellX;
            CellY = cellY;
            id = CellX * 100 + CellY;
            SurroundingCells = new List<Cell>();

            leftX = cellX * WIDTH;
            rightX = leftX + WIDTH;
            bottomY = cellY * HEIGHT;
            topY = topY + HEIGHT;

            ContainedPlayers = new List<Player>();
            ContainedFood = new List<Food>();
        }

        public void AddPlayer(Player player)
        {
            ContainedPlayers.Add(player);
            player.OnPlayerDied += (s, ea) => RemovePlayer(ea.DeadPlayer);
        }

        public void AddFood(Food food)
        {
            ContainedFood.Add(food);
            food.OnFoodEaten += (s, ea) => RemoveFood(ea.Eaten);
        }

        public void Update()
        {
            List<Player> newPlayers;
            List<Food> newFood;
            CheckSurroundingCells(out newPlayers, out newFood);

            for (int i = 0; i < ContainedPlayers.Count; i++)
            {
                Player player = ContainedPlayers[i];
                if (!PlayerIsIn(player))
                {
                    ContainedPlayers.Remove(player);
                    OnPlayerLeft?.Invoke(this, new PlayerLeftEventArgs(ContainedPlayers, ContainedFood, player.Id));
                }
            }
        }

        public void Clear()
        {
            ContainedPlayers.Clear();
            ContainedFood.Clear();
        }

        public bool PlayerIsIn(Player player)
        {
            foreach (Circle circle in player.PlayerCircles)
            {
                if (EatableObjectIsIn(circle))
                {
                    return true;
                }
            }

            return false;
        }

        public bool EatableObjectIsIn(EatableObject eatableObject)
        {
            double eatableObjectLeftX = eatableObject.Position.X - eatableObject.Radius;
            double eatableObjectRightX = eatableObject.Position.X + eatableObject.Radius;
            double eatableObjectBottomY = eatableObject.Position.Y - eatableObject.Radius;
            double eatableObjectTopY = eatableObject.Position.Y + eatableObject.Radius;

            bool horizontallyInside = (leftX <= eatableObjectLeftX) && (eatableObjectRightX <= rightX);
            bool verticallyInside = (bottomY <= eatableObjectBottomY) && (eatableObjectTopY <= topY);

            return horizontallyInside && verticallyInside;
        }

        private void CheckSurroundingCells(out List<Player> newPlayers, out List<Food> newFood)
        {
            newPlayers = new List<Player>();
            newFood = new List<Food>();

            for (int i = 0; i < SurroundingCells.Count; i++)
            {
                for (int j = 0; j < SurroundingCells[i].ContainedPlayers.Count; j++)
                {
                    Player player = SurroundingCells[i].ContainedPlayers[j];
                    if (PlayerIsIn(player))
                    {
                        newPlayers.Add(player);
                        OnPlayerAdded?.Invoke(this, new PlayerAddedEventArgs(ContainedPlayers, ContainedFood, player.Id));
                    }
                }
            }
        }

        private void RemovePlayer(Player player)
        {
            ContainedPlayers.Remove(player);
        }

        private void RemoveFood(Food food)
        {
            ContainedFood.Remove(food);
        }
    }
}
