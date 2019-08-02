using System.Drawing;
using System.Collections.Generic;

namespace TravelEurope
{
    /// <summary>
    /// Represents node in a graph
    /// </summary>
    public class City : IDrawable
    {
        #region Instance variables

        public readonly string Name;
        public readonly Country Country;
        public readonly List<Road> Roads;
        public Point Location;

        #endregion

        public City(string Name, Country Country, int x, int y)
        {
            this.Name = Name;
            this.Country = Country;
            Roads = new List<Road>();
            Location = new Point(x,y);
        }

        //draw a point on the bitmap with specific color    
        public void Draw(Graphics g, Color c)
        {
            g.DrawRectangle(new Pen(c, 5), Location.GetX(), Location.GetY(), 2, 2);
        }
    }
}
