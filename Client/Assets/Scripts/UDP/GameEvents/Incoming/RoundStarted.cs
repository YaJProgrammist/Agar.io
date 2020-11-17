using System;
using System.Collections.Generic;
using UnityEngine;

namespace Server.Events
{
    public class RoundStarted : IncomingGameEvent
    {
        private List<int> playersId;
        private List<int> circleId;
        private List<double> circleX;
        private List<double> circleY;

        public RoundStarted(byte[] package)
        {
            if (package[0] != (byte)IncomingGameEventTypes.RoundStarted)
            {
                Debug.LogError("Incorrect package");
                return;
            }

            playersId = new List<int>();
            circleId = new List<int>();
            circleX = new List<double>();
            circleY = new List<double>();

            int i = 0;
            while (i < package.Length)
            {
                playersId.Add(Deserializer.DeserializeInt(package, i));
                i += 4;

                circleId.Add(Deserializer.DeserializeInt(package, i));
                i += 4;

                circleX.Add(Deserializer.DeserializeDouble(package, i));
                i += 4;

                circleY.Add(Deserializer.DeserializeDouble(package, i));
                i += 4;
            }
        }

        public override void Handle()
        {
            
        }
    }
}
