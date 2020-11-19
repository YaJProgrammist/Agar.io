using System;

public static class IncomingPackagesManager
{
    public static void HandlePackage(byte[] package, int playerId = -1)
    {
        try
        {
            IncomingGameEvent gameEvent;

            switch ((IncomingGameEventTypes)package[0])
            {
                case IncomingGameEventTypes.RoundStarted:
                    gameEvent = new RoundStarted(package);
                    break;
                case IncomingGameEventTypes.PlayerAdded:
                    gameEvent = new PlayerAdded(package);
                    break;
                default:
                    throw new Exception(String.Format("Incorrect package type: {0}", (IncomingGameEventTypes)package[0]));
                    break;
            }

            gameEvent.Handle();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
