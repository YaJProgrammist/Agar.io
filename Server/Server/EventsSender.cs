using Server.Events;

namespace Server
{
    public static class EventsSender
    {
        public static void RegisterEvent(OutgoingGameEvent gameEvent)
        {
            byte[] message = gameEvent.GetSerialized();
            UDPServer.GetInstance().SendMessageToAll(message, message.Length);
        }

        public static void RegisterEvent(OutgoingGameEvent gameEvent, int receiverPlayerID)
        {
            byte[] message = gameEvent.GetSerialized();
            UDPServer.GetInstance().SendMessageToPlayer(message, message.Length, receiverPlayerID);
        }
    }
}
