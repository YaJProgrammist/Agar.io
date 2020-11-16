using System;
using System.Collections.Generic;

namespace Server
{
    public static class Serializer
    {
        public static List<byte> SerializeInt(int initialValue)
        {
            List<byte> seriaized = new List<byte>(4);

            for (int i = 0; i < 4; i++)
            {
                seriaized[i] = (byte)(initialValue % 256);
                initialValue /= 256;
            }

            return seriaized;
        }
        public static List<byte> SerializeDouble(double initialValue)
        {
            return SerializeInt((int)Math.Round(initialValue * 100));
        }
    }
}
