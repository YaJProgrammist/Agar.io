using Server.Events;

namespace Server
{
    public static class EventsSender
    {
        public static void RegisterEvent(GameEvent gameEvent)
        {
            byte[] message = gameEvent.GetSerialized();
            UDPServer.GetInstance().Send(message, message.Length);
        }

        public static void RegisterEvent(GameEvent gameEvent, int receiverPlayerID)
        {
            byte[] message = gameEvent.GetSerialized();
            UDPServer.GetInstance().Send(message, message.Length);
        }
    }
}
