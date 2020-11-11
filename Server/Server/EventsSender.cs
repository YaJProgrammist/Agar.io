using Server.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class EventsSender
    {
        private static EventsSender instance = null;
        private static readonly object lockObj = new object();
        private int prevCircleId;

        private EventsSender()
        {
        }

        public static EventsSender GetInstance()
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new EventsSender();
                }
                return instance;
            }
        }

        public void RegisterEvent(GameEvent gameEvent)
        {
            gameEvent.GetSerialized();
        }
    }
}
