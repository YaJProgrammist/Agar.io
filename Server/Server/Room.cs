using Server.Events;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Room
    {
        public const int UPDATE_TIME_MS = 200;

        private List<Cell> cells;
        private List<Player> players;
        private List<Circle> circles;
        private Task gameRunningTask;

        public bool IsGameRunning { get; private set; }

        public Room()
        {
            cells = new List<Cell>();
            players = new List<Player>();
            IsGameRunning = false;
        }

        public void StartGame()
        {
            IsGameRunning = true;
            gameRunningTask = new Task(RunGame);
            gameRunningTask.Start();
        }

        public void StopGame()
        {
            if (IsGameRunning)
            {
                IsGameRunning = false;
            }
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
            if (!IsGameRunning)
            {
                return;            
            }

            UpdatePositions();
            UpdateEatenObjects();

            if (IsRoundOver())
            {
                StopGame();
            }
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
            return players.Count <= 1;
        }
    }
}
