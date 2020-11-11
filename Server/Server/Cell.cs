using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class Cell
    {
        private SortedSet<EatableObject> containedObjects;

        public Cell()
        {
            containedObjects = new SortedSet<EatableObject>();
        }
    }
}
