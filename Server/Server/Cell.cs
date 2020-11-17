using System.Collections.Generic;

namespace Server
{
    public class Cell
    {
        public const double HEIGHT = 100;
        public const double WIDTH = 100;
        private SortedSet<Circle> containedCircles;
        private SortedSet<Food> containedFood;

        public Cell()
        {
            containedCircles = new SortedSet<Circle>();
            containedFood = new SortedSet<Food>();
        }
    }
}
