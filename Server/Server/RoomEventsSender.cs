using Server.Events;

namespace Server
{
    public class RoomEventsSender
    {
        public RoomEventsSender(Room room)
        {
            room.OnGameEventOccured += HandleGameEvent;
        }

        private void HandleGameEvent(object sender, GameEventOccuredEventArgs eventArgs)
        {
            if (eventArgs.PlayerId == null)
            {
                RegisterEvent(eventArgs.GameEvent);
            }
            else
            {
                RegisterEvent(eventArgs.GameEvent, (int)eventArgs.PlayerId);
            }
        }

        private void RegisterEvent(OutgoingGameEvent gameEvent)
        {
            byte[] message = gameEvent.GetSerialized();
            UDPServer.GetInstance().SendMessageToAll(message, message.Length);
        }

        private void RegisterEvent(OutgoingGameEvent gameEvent, int receiverPlayerID)
        {
            byte[] message = gameEvent.GetSerialized();
            UDPServer.GetInstance().SendMessageToPlayer(message, message.Length, receiverPlayerID);
        }
    }
}
