using System;

namespace Server.Events
{
    public class Split : IncomingGameEvent
    {
        private int playerId;

        public Split(byte[] package)
        {
            if (package.Length != 5 || package[0] != (byte)IncomingGameEventTypes.Split)
            {
                Console.WriteLine("Incorrect package");
                return;
            }

            playerId = Deserializer.DeserializeInt(package, 1);
        }

        public override void Handle(Room room)
        {
            room.SplitPlayer(playerId);
        }
    }
}
