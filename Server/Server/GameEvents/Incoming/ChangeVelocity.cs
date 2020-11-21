using System;

namespace Server.Events
{
    public class ChangeVelocity : IncomingGameEvent
    {
        private int playerId;
        private double velocityX;
        private double velocityY;

        public ChangeVelocity(byte[] package)
        {
            if (package.Length != 7 || package[0] != (byte)IncomingGameEventTypes.ChangeVelocity)
            {
                Console.WriteLine("Incorrect package");
                return;
            }

            playerId = Deserializer.DeserializeInt(package, 1);
            velocityX = Deserializer.DeserializeNormalizedVectorCoord(package, 5);
            velocityY = Deserializer.DeserializeNormalizedVectorCoord(package, 6);

            Console.WriteLine("Player id", playerId);

            Console.WriteLine();
        }

        public override void Handle(Room room)
        {
            room.SetPlayerVelocity(playerId, velocityX, velocityY);
        }
    }
}
