public static class EventsSender
{
    public static void RegisterEvent(OutgoingGameEvent gameEvent)
    {
        byte[] message = gameEvent.GetSerialized();
        UDPClient.SendMessage(message, message.Length);
    }
}
