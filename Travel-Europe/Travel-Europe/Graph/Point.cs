namespace TravelEurope
{
    public class Point
    {
        protected int x;
        protected int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetX() => x;
        public int GetY() => y;
    }
}
