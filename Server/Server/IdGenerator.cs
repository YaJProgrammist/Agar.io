namespace Server
{
    public class IdGenerator
    {
        private static IdGenerator instance = null;
        private static readonly object lockObj1 = new object();
        private static readonly object lockObj2 = new object();
        private int lastId;

        private IdGenerator()
        {
            lastId = 0;
        }

        public static IdGenerator GetInstance()
        {
            lock (lockObj1)
            {
                if (instance == null)
                {
                    instance = new IdGenerator();
                }

                return instance;
            }
        }

        public int GetId()
        {
            lock (lockObj2)
            {
                lastId++;
                return lastId;
            }
        }
    }
}
