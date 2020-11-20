using System;

namespace Server.Events
{
    public class LeaveGame : IncomingGameEvent
    {
        private int playerId;

        public LeaveGame(byte[] package)
        {
            if (package.Length != 5 || package[0] != (byte)IncomingGameEventTypes.LeaveGame)
            {
                Console.WriteLine("Incorrect package");
                return;
            }

            playerId = Deserializer.DeserializeInt(package, 1);
        }

        public override void Handle()
        {
            Room.GetInstance().RemovePlayer(playerId);
            UDPServer.GetInstance().RemovePlayerClient(playerId);
        }
    }
}
