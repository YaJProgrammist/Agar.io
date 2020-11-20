using System;

namespace Server.Events
{
    public class ChangeVelocity : IncomingGameEvent
    {
        private int playerId;
        private int velocityX;
        private int velocityY;

        public ChangeVelocity(byte[] package)
        {
            if (package.Length != 13 || package[0] != (byte)IncomingGameEventTypes.ChangeVelocity)
            {
                Console.WriteLine("Incorrect package");
                return;
            }

            playerId = Deserializer.DeserializeInt(package, 1);
            velocityX = Deserializer.DeserializeInt(package, 5);
            velocityY = Deserializer.DeserializeInt(package, 9);
        }

        public override void Handle(Room room)
        {
            room.SetPlayerVelocity(playerId, velocityX, velocityY);
        }
    }
}
