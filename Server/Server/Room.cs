using Server.Events;
using Server.Events.Incoming;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Server
{
    public class Room
    {
        public const double HEIGHT = 1000;
        public const double WIDTH = 1000;
        public const int UPDATE_TIME_MS = 200;
        public const int WAITING_TIME_BEFORE_ROUND_MS = 10000;
        private const int FOOD_AMOUNT_PER_UPDATE = 100;
        private const int FOOD_AMOUNT_ON_START = 1000;
        private const double WIN_SCORE = 200;

        private List<List<Cell>> cells;
        private List<Player> players;
        private List<Player> playersWaitingForNextRound;
        private List<Food> food;
        private Thread gameRunningThread;
        private Random rand;

        public bool IsGameRunning { get; private set; }

        public event EventHandler<GameEventOccuredEventArgs> OnGameEventOccured;

        public Room()
        {
            cells = new List<List<Cell>>();
            players = new List<Player>();
            IsGameRunning = false;
            rand = new Random();
            food = new List<Food>();
            playersWaitingForNextRound = new List<Player>();
            InitializeCells();

            IncomingPackagesManager.OnPackageIncame += (s, ea) => ea.GameEvent.Handle(this);
        }

        public void StartGame()
        {
            gameRunningThread = new Thread(RunGame);
            gameRunningThread.Start();
        }

        public void StopGame()
        {
            if (IsGameRunning)
            {
                IsGameRunning = false;
            }

            RoundOver roundOverGameEvent = new RoundOver(players);
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(roundOverGameEvent));
        }

        public void AddPlayer(Player player)
        {
            playersWaitingForNextRound.Add(player);

            PlayerAdded playerAddedGameEvent = new PlayerAdded(player.Id);
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(playerAddedGameEvent, player.Id));
        }

        public void RemovePlayer(int playerId)
        {
            Player player = players.Find(player => player.Id == playerId);
            players.Remove(player);
        }

        public void SplitPlayer(int playerId)
        {
            players.Find(player => player.Id == playerId).Split();
        }

        public void SetPlayerVelocity(int playerId, double velocityX, double velocityY)
        {
            players.Find(player => player.Id == playerId).SetVelocity(velocityX, velocityY);
        }

        private void OnPlayerDied(object sender, PlayerDiedEventArgs eventArgs)
        {
            playersWaitingForNextRound.Add(eventArgs.DeadPlayer);
            players.Remove(eventArgs.DeadPlayer);

            PlayerDied playerDiedGameEvent = new PlayerDied(eventArgs.DeadPlayer.Id);
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(playerDiedGameEvent, eventArgs.DeadPlayer.Id));
        }

        private void OnPlayerCirclesAdded(object sender, PlayerCirclesAddedEventArgs eventArgs)
        {
            CirclesAdded circlesAddedGameEvent = new CirclesAdded(eventArgs.NewCircles, eventArgs.PlayerId);
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(circlesAddedGameEvent));
        }

        private void OnPlayerCirclesRemoved(object sender, PlayerCirclesRemovedEventArgs eventArgs)
        {
            CirclesRemoved circlesRemovedGameEvent = new CirclesRemoved(eventArgs.NewCircles.ConvertAll(circle => circle.Id));
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(circlesRemovedGameEvent));
        }

        private void InitializeCells()
        {
            int cellNumXMax = (int)(WIDTH / Cell.WIDTH);
            int cellNumYMax = (int)(HEIGHT / Cell.HEIGHT);

            for (int cellNumX = 0; cellNumX <= cellNumXMax; cellNumX++)
            {
                cells.Add(new List<Cell>());
                for (int cellNumY = 0; cellNumY <= cellNumYMax; cellNumY++)
                {
                    Cell newCell = new Cell(cellNumX, cellNumY);
                    cells[cellNumX].Add(newCell);

                    if (cellNumX > 0)
                    {
                        cells[cellNumX - 1][cellNumY].SurroundingCells.Add(newCell);
                        newCell.SurroundingCells.Add(cells[cellNumX - 1][cellNumY]);
                    }

                    if (cellNumY > 0)
                    {
                        cells[cellNumX][cellNumY - 1].SurroundingCells.Add(newCell);
                        newCell.SurroundingCells.Add(cells[cellNumX][cellNumY - 1]);
                    }

                    if (cellNumX > 0 && cellNumY > 0)
                    {
                        cells[cellNumX - 1][cellNumY - 1].SurroundingCells.Add(newCell);
                        newCell.SurroundingCells.Add(cells[cellNumX - 1][cellNumY - 1]);
                    }

                    if (cellNumX > 0 && cellNumY < cellNumYMax - 1)
                    {
                        cells[cellNumX - 1][cellNumY + 1].SurroundingCells.Add(newCell);
                        newCell.SurroundingCells.Add(cells[cellNumX - 1][cellNumY + 1]);
                    }
                }
            }
        }

        private void UpdateAllCells()
        {
            foreach(List<Cell> cellRow in cells)
            {
                foreach (Cell cell in cellRow)
                {
                    cell.Update();
                }
            }
        }

        private void ClearAllCells()
        {
            foreach (List<Cell> cellRow in cells)
            {
                foreach (Cell cell in cellRow)
                {
                    cell.Clear();
                }
            }
        }

        private void GenerateplayersFirstCircles()
        {
            Random rand = new Random();

            for (int i = 0; i < players.Count; i++)
            {
                Point playerPosition;

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
                int cellX = Math.Min((int)(playerPosition.X / Cell.WIDTH), cells.Count);
                int cellY = Math.Min((int)(playerPosition.Y / Cell.HEIGHT), cells.Count);
                cells[cellX][cellY].AddPlayer(players[i]);
            }
        }

        private Point GetRandomPointInRoom()
        {
            double x = rand.Next((int)(WIDTH * 1000)) / 1000.0;
            double y = rand.Next((int)(HEIGHT * 1000)) / 1000.0;

            return new Point(x, y);
        }

        private void RunGame()
        {
            while (true)
            {
                Thread.Sleep(WAITING_TIME_BEFORE_ROUND_MS);
                RefreshRoom();
                IsGameRunning = true;

                while (IsGameRunning)
                {
                    Thread.Sleep(UPDATE_TIME_MS);
                    Update();
                }                
            }
        }

        private void RefreshRoom()
        {
            players.AddRange(playersWaitingForNextRound);
            playersWaitingForNextRound.Clear();
            food.Clear();
            ClearAllCells();

            foreach(Player player in players)
            {
                SubscribeOnPlayerEvents(player);
            }

            GenerateplayersFirstCircles();

            Console.WriteLine("Round started");
            RoundStarted roundStartedGameEvent = new RoundStarted(players);
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(roundStartedGameEvent));

            GenerateFood(FOOD_AMOUNT_ON_START);
        }

        private void SubscribeOnPlayerEvents(Player player)
        {
            player.OnPlayerDied += OnPlayerDied;
            player.OnPlayerCirclesAdded += OnPlayerCirclesAdded;
            player.OnPlayerCirclesRemoved += OnPlayerCirclesRemoved;
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

            players.Sort();

            CirclesFrameUpdate positionsUpdatedGameEvent = new CirclesFrameUpdate(players);
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(positionsUpdatedGameEvent));

            GenerateFood(FOOD_AMOUNT_PER_UPDATE);

            if (IsRoundOver())
            {
                StopGame();
            }

            UpdateAllCells();

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
            List<Food> newFood = new List<Food>();

            for (int i = 0; i < amount; i++)
            {
                Point newFoodPoint = GetRandomPointInRoom();
                Food newFoodItem = new Food(newFoodPoint);
                newFood.Add(newFoodItem);

                int cellX = Math.Min((int)(newFoodItem.Position.X / Cell.WIDTH), cells.Count);
                int cellY = Math.Min((int)(newFoodItem.Position.Y / Cell.HEIGHT), cells.Count);
                cells[cellX][cellY].AddFood(newFoodItem);
            }

            food.AddRange(newFood);

            FoodAdded foodAddedGameEvent = new FoodAdded(newFood);
            OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(foodAddedGameEvent));
        }

        private void UpdatePositions()
        {
            foreach (Player player in players)
            {
                player.Move(0, WIDTH, HEIGHT, 0);
            }
        }

        private void UpdateEatenObjects()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            List<EatPair<Circle>> eatPairsCircles = new List<EatPair<Circle>>();
            List<EatPair<Food>> eatPairsFood = new List<EatPair<Food>>();

            for (int playerInd1 = 0; playerInd1 < players.Count; playerInd1++)
            {
                for (int playerInd2 = playerInd1 + 1; playerInd2 < players.Count; playerInd2++)
                {
                    eatPairsCircles.AddRange(players[playerInd1].CalculateCirclesEatPairs(players[playerInd2]));
                    eatPairsCircles.AddRange(players[playerInd2].CalculateCirclesEatPairs(players[playerInd1]));
                }
                
                foreach (Food foodItem in food)
                {
                    eatPairsFood.AddRange(players[playerInd1].CalculateFoodEatPairs(foodItem));
                }
            }

            if (eatPairsCircles.Count > 0)
            {
                CirclesRemoved circlesRemovedGameEvent = new CirclesRemoved(eatPairsCircles.ConvertAll<int>(eatPair => eatPair.Eaten.Id));
                OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(circlesRemovedGameEvent));
            }

            if (eatPairsFood.Count > 0)
            {
                FoodRemoved foodRemovedGameEvent = new FoodRemoved(eatPairsFood.ConvertAll<int>(eatPair => eatPair.Eaten.Id));
                OnGameEventOccured?.Invoke(this, new GameEventOccuredEventArgs(foodRemovedGameEvent));
            }

            foreach (EatPair<Circle> pair in eatPairsCircles)
            {
                pair.Eater.EatObject(pair.Eaten);
            }

            foreach (EatPair<Food> pair in eatPairsFood)
            {
                pair.Eater.EatObject(pair.Eaten);
                food.Remove(pair.Eaten);
            }
        }

        private bool IsRoundOver()
        {
            if (players.Count == 0 || players[0].Score >= WIN_SCORE)
            {
                return true;
            }

            return false;
        }
    }
}
