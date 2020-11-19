using System;

namespace Server.Events
{
    public class Split : IncomingGameEvent
    {
        private string playerIP;

        public Split(byte[] package)
        {
            if (package[0] != (byte)IncomingGameEventTypes.Split || package.Length != 5)
            {
                Console.WriteLine("Incorrect package");
                return;
            }

            playerIP = Deserializer.DeserializeInt(package, 4);
        }

        public override void Handle()
        {
            Console.WriteLine("Player {0} is here!", playerIP); // TODO delete
            int playerId = IdGenerator.GetInstance().GetId();
            Room.GetInstance().AddPlayer(new Player(playerId));
            UDPServer.GetInstance().AddPlayerClient(playerId, new Address(PLAYERS_PORT, playerIP));
        }
    }
}
