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
            g.DrawEllipse(new Pen(c, 5), Location.X, Location.Y, 3, 3);
        }

        public bool Contains(int x, int y)
        {
            Rectangle hitbox = new Rectangle(Location.X - 1, Location.Y - 1, 5, 5);

            return hitbox.Contains(x, y);
        }
    }
}
