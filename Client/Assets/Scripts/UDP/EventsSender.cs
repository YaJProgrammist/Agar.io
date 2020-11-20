public static class EventsSender
{
    public static void RegisterEvent(OutgoingGameEvent gameEvent)
    {
        byte[] message = gameEvent.GetSerialized();
        UDPClient.GetInstance().SendMessage(message, message.Length);
    }
}
