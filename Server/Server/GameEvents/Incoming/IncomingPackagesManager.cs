using System;

namespace Server.Events.Incoming
{
    public static class IncomingPackagesManager
    {
        public static event EventHandler<OnPackageIncameEventArgs> OnPackageIncame;

        public static void HandlePackage(byte[] package, int playerId = -1)
        {
            try
            {
                IncomingGameEvent gameEvent;

                switch ((IncomingGameEventTypes)package[0])
                {
                    case IncomingGameEventTypes.ChangeVelocity:
                        gameEvent = new ChangeVelocity(package);
                        break;
                    case IncomingGameEventTypes.Split:
                        gameEvent = new Split(package);
                        break;
                    case IncomingGameEventTypes.ConnectionToServer:
                        gameEvent = new ConnectionToServer(package);
                        break;
                    case IncomingGameEventTypes.LeaveGame:
                        gameEvent = new LeaveGame(package);
                        break;
                    default:
                        throw new Exception(String.Format("Incorrect package type: {0}", (IncomingGameEventTypes)package[0]));
                        break;
                }

                OnPackageIncame?.Invoke(null, new OnPackageIncameEventArgs(gameEvent));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
