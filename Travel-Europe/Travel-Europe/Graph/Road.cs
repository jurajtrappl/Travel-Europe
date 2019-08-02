using System;
using System.Drawing;

namespace TravelEurope
{
    /// <summary>
    /// Represents edge in a graph
    /// </summary>
    public class Road : IDrawable
    {
        #region Instance variables 

        public readonly City StartCity;
        public readonly City DestinationCity;
        public int Distance;
        public readonly int MaxAllowedSpeed;

        #endregion

        public Road(City startCity, City destinationCity, int maxAllowedSpeed)
        {
            StartCity = startCity;
            DestinationCity = destinationCity;
            Distance = AssignDistance(startCity, destinationCity);
            MaxAllowedSpeed = maxAllowedSpeed;
        }

        public void Draw(Graphics g, Color c)
        {
            Pen lineColor = new Pen(c, 2);
            
            //find the coordinates of start and end points
            Point stPoint = StartCity.Location;
            Point endPoint = DestinationCity.Location;

            g.DrawLine(lineColor, stPoint.GetX(), stPoint.GetY(), endPoint.GetX(), endPoint.GetY());
        }

        int AssignDistance(City startCity, City destinationCity)
        {
            int sx = startCity.Location.GetX();
            int sy = startCity.Location.GetY();
            int dx = destinationCity.Location.GetX();
            int dy = destinationCity.Location.GetY();

            return CalculateDistance(sx, sy, dx, dy);
        }

        int CalculateDistance(int x1, int y1, int x2, int y2)
        {
            double pixelsBetween = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double kilometers = 1.65 * pixelsBetween;
            //1px on the map is 16,5m...0,0165km
            return (int)kilometers;
        }
    }
}
