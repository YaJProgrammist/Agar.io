using UnityEngine;

namespace Server.Events
{
    public class PlayerAdded : IncomingGameEvent
    {
        private int myPlayerId;

        public PlayerAdded(byte[] package)
        {
            if (package[0] != (byte)IncomingGameEventTypes.PlayerAdded)
            {
                Debug.LogError("Incorrect package");
                return;
            }

            myPlayerId = Deserializer.DeserializeInt(package, 0);
        }

        public override void Handle()
        {
            Debug.Log("MY ID: " + myPlayerId.ToString()); // TODO delete
        }
    }
}
