using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public static class Serializer
    {
        public static List<byte> SerializeInt(int initialValue)
        {
            List<byte> seriaized = new List<byte>();

            while (initialValue > 0)
            {
                seriaized.Add((byte)(initialValue % 256));
                initialValue /= 256;
            }

            return seriaized;
        }

        public static List<byte> SerializeDouble(int initialValue)
        {
            List<byte> seriaized = new List<byte>();

            while (initialValue > 0)
            {
                seriaized.Add((byte)(initialValue % 256));
                initialValue /= 256;
            }

            return seriaized;
        }
    }
}
