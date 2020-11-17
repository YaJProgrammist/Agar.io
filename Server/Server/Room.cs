using Server.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Room
    {
        public const double HEIGHT = 10000;
        public const double WIDTH = 10000;
        public const int UPDATE_TIME_MS = 200;
        private const int FOOD_AMOUNT_PER_UPDATE = 100;
        private const int FOOD_AMOUNT_ON_START = 1000;

        private List<Cell> cells;
        private List<Player> players;
        private List<EatableObject> food;
        private Thread gameRunningThread;
        private Random rand;

        public bool IsGameRunning { get; private set; }

        public Room()
        {
            cells = new List<Cell>();
            players = new List<Player>();
            IsGameRunning = false;
            rand = new Random();
            food = new List<EatableObject>();

            // TODO delete
            players.Add(new Player());
            players.Add(new Player());
            players.Add(new Player());
            players.Add(new Player());
            players.Add(new Player());
            players.Add(new Player());

            players[0].SetVelocity(0.5, 0.5);
            players[1].SetVelocity(-0.5, -0.25);
            players[2].SetVelocity(1, 1);
            players[3].SetVelocity(-0.1, 0.6);
            players[4].SetVelocity(-0.7, 0.7);
            players[5].SetVelocity(1, 0);
            // TODO delete

        }

        public void StartGame()
        {
            IsGameRunning = true;
            GeneratePlayersFirstCircles();
            GenerateFood(FOOD_AMOUNT_ON_START);
            gameRunningThread = new Thread(RunGame);
            gameRunningThread.Start();
        }

        public void StopGame()
        {
            if (IsGameRunning)
            {
                IsGameRunning = false;
            }
        }

        private void GeneratePlayersFirstCircles()
        {
            Random rand = new Random();

            for (int i = 0; i < players.Count; i++)
            {
                Point playerPosition = GetRandomPointInRoom();

                while (true)
                {
                    playerPosition = GetRandomPointInRoom();

                    bool canBeEatenByOtherPlayer = false;

                    for (int j = 0; j < i; j++)
                    {
                        Circle otherPlayerCicle = players[j].PlayerCircles[0];

                        bool horizontallyInside = (otherPlayerCicle.Position.X - otherPlayerCicle.Radius < playerPosition.X) && 
                                                    (playerPosition.X < otherPlayerCicle.Position.X + otherPlayerCicle.Radius);

                        bool verticallyInside = (otherPlayerCicle.Position.Y - otherPlayerCicle.Radius < playerPosition.Y) && 
                                                    (playerPosition.Y < otherPlayerCicle.Position.Y + otherPlayerCicle.Radius);

                        if (horizontallyInside && verticallyInside)
                        {
                            canBeEatenByOtherPlayer = true;
                            break;
                        }
                    }

                    if (!canBeEatenByOtherPlayer)
                    {
                        break;
                    }
                }

                players[i].StartNewGame(playerPosition);
            }

            EventsSender.RegisterEvent(new RoundStarted(players));
        }

        private Point GetRandomPointInRoom()
        {
            double x = rand.Next((int)(WIDTH * 1000)) / 1000.0;
            double y = rand.Next((int)(HEIGHT * 1000)) / 1000.0;

            return new Point(x, y);
        }

        private void RunGame()
        {
            while (IsGameRunning)
            {
                Thread.Sleep(UPDATE_TIME_MS);
                Update();
            }
        }

        private void Update()
        {
            // TODO delete
            Console.WriteLine("Update");
            // TODO delete

            if (!IsGameRunning)
            {
                return;            
            }

            UpdatePositions();
            UpdateEatenObjects();
            GenerateFood(FOOD_AMOUNT_PER_UPDATE);

            if (IsRoundOver())
            {
                StopGame();
            }

            // TODO delete
            foreach (Player player in players)
            {
                Console.WriteLine("Player {0}: ({1:0.00}; {2:0.00}) | {3}", player.Id, player.PlayerCircles[0].Position.X, player.PlayerCircles[0].Position.Y, player.PlayerCircles[0].Radius);
            }
            // TODO delete
        }

        private void GenerateFood(int amount)
        {
            Random rand = new Random();
            List<EatableObject> newFood = new List<EatableObject>();

            for (int i = 0; i < amount; i++)
            {
                Point newFoodPoint = GetRandomPointInRoom();
                newFood.Add(new Food(newFoodPoint));
            }

            food.AddRange(newFood);

            EventsSender.RegisterEvent(new EatableObjectsAdded(newFood));
        }

        private void UpdatePositions()
        {
            foreach (Player player in players)
            {
                player.Move();
            }

            PositionsUpdated positionsUpdatedEvent = new PositionsUpdated(players);
            EventsSender.RegisterEvent(positionsUpdatedEvent);
        }

        private void UpdateEatenObjects()
        {
            Dictionary<Circle, EatableObject> eatPairs = new Dictionary<Circle, EatableObject>();

            for (int playerInd1 = 0; playerInd1 < players.Count; playerInd1++)
            {
                for (int playerInd2 = playerInd1 + 1; playerInd2 < players.Count; playerInd2++)
                {
                    players[playerInd1].TryEatPlayer(players[playerInd2], ref eatPairs);
                    players[playerInd2].TryEatPlayer(players[playerInd1], ref eatPairs);
                }
            }

            foreach (var pair in eatPairs)
            {
                pair.Key.EatObject(pair.Value);
            }

            players.Sort();

            PositionsUpdated positionsUpdatedEvent = new PositionsUpdated(players);
            EventsSender.RegisterEvent(positionsUpdatedEvent);
        }

        private bool IsRoundOver()
        {
            return players.Count <= 1; //TODO Radius == half of cell width or height
        }
    }
}
