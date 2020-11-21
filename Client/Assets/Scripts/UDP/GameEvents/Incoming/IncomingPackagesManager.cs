using System;
using UnityEngine;

public static class IncomingPackagesManager
{
    public static void HandlePackage(byte[] package, int playerId = -1)
    {
        try
        {
            IncomingGameEvent gameEvent;

            Debug.Log((IncomingGameEventTypes)package[0]);

            switch ((IncomingGameEventTypes)package[0])
            {
                case IncomingGameEventTypes.CirclesFrameUpdate:
                    gameEvent = new CirclesFrameUpdate(package);
                    break;
                case IncomingGameEventTypes.RoundStarted:
                    gameEvent = new RoundStarted(package);
                    break;
                case IncomingGameEventTypes.PlayerAdded:
                    gameEvent = new PlayerAdded(package);
                    break;
                case IncomingGameEventTypes.PlayerDied:
                    gameEvent = new PlayerDied(package);
                    break;
                case IncomingGameEventTypes.RoundOver:
                    gameEvent = new RoundOver(package);
                    break;
                case IncomingGameEventTypes.CirclesAdded:
                    gameEvent = new CirclesAdded(package);
                    break;
                case IncomingGameEventTypes.CirclesRemoved:
                    gameEvent = new CirclesRemoved(package);
                    break;
                case IncomingGameEventTypes.FoodAdded:
                    gameEvent = new FoodAdded(package);
                    break;
                case IncomingGameEventTypes.FoodRemoved:
                    gameEvent = new FoodRemoved(package);
                    break;
                case IncomingGameEventTypes.PlayerLeft:
                    gameEvent = new PlayerLeft(package);
                    break;
                default:
                    throw new Exception(String.Format("Incorrect package type: {0}", (IncomingGameEventTypes)package[0]));
                    break;
            }

            GameManager.Instance.GameEvents.Enqueue(gameEvent);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
