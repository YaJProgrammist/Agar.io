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
            List<Player> newStatePlayers = players;

            for (int playerInd1 = 0; playerInd1 < players.Count; playerInd1++)
            {
                for (int playerInd2 = playerInd1 + 1; playerInd2 < players.Count; playerInd2++)
                {
                    
                }
            }

            newStatePlayers.Sort();
            players = newStatePlayers;

            PositionsUpdated positionsUpdatedEvent = new PositionsUpdated(players);
            EventsSender.RegisterEvent(positionsUpdatedEvent);
        }

        private bool IsRoundOver()
        {

        }
    }
}
