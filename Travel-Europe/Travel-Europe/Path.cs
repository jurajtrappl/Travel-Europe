using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TravelEurope
{
    class Path
    {
        public List<City> TravelPath;
        int CityCount() => TravelPath.Count;

        public Path(Map map, City startCity, City destinationCity, Queue<City> cityBetween)
        {
            TravelPath = new List<City>();

            //just start and end city
            if (cityBetween.Count == 0)
            {
                //TravelPath = Dijkstra.ShortestPath(map, startCity, destinationCity);
                TravelPath = Dijkstra.ShortestPath(map, startCity, destinationCity);
            }
            else
            {
                //we have cities between
                TravelPath.Add(startCity);

                while (cityBetween.Count > 0)
                {
                    City lastDequeued = cityBetween.Dequeue();

                    AddPathTo(map, lastDequeued);
                }

                //add path to end city finally

                AddPathTo(map, destinationCity);
            }
        }
        
        void AddPathTo(Map map, City pathTo)
        {
            City lastCity = TravelPath[CityCount() - 1];

            List<City> newPath = Dijkstra.ShortestPath(map, lastCity, pathTo);

            //Dont add 0., it will create duplicates
            for (int i = 1; i < newPath.Count; i++)
            {
                TravelPath.Add(newPath[i]);
            }
        }

        //design addition
        void Separator(RichTextBox output) => 
            output.Text += Environment.NewLine + "----------------------------------------------------" + Environment.NewLine;

        public void PathSummary(RichTextBox output)
        {
            //SHOW THE PATH
            output.Text += "Trip from " + TravelPath[0].Name.ToUpper() + " to " + TravelPath[CityCount() - 1].Name.ToUpper();
            PrintPath(output);

            //CALCULATIONS
            //calculates the distance between the start and destination citiy
            output.Text += "Total distance to travel: " + TotalDistance() + "km" + Environment.NewLine;
            //calculates the duration of travelling
            Time time = new Time(TotalDuration());
            output.Text += "Overall duration: " + time.Hours + "h " + time.Minutes + "min";

            //travel summary
            CitiesOnPath(output);
        }

        /// <summary>
        /// Computes the total distance between the Cities in the given list
        /// </summary>
        /// <returns></returns>
        int TotalDistance()
        {
            int tDistance = 0;

            for (int i = 0; i < CityCount() - 1; i++)
            {
                Road current = FindTheRoad(TravelPath[i], TravelPath[i + 1]);
                tDistance += current.Distance;
            }

            return tDistance;
        }

        /// <summary>
        /// Computes the total time spent by travelling on the path
        /// </summary>
        /// <returns></returns>
        double TotalDuration()
        {
            double duration = 0;

            for (int i = 0; i < CityCount() - 1; i++)
            {
                Road curr = FindTheRoad(TravelPath[i], TravelPath[i + 1]);
                ComputeDuration(ref duration, curr.MaxAllowedSpeed, curr.Distance);
            }

            return duration;
        }

        void ComputeDuration(ref double duration, int maxSpeed, int distance)
        {
            if (Car.Instance.MaxSpeed >= maxSpeed)
                duration += distance / (double)maxSpeed;
            else
                duration += distance / (double)Car.Instance.MaxSpeed;
        }

        /// <summary>
        /// Finds the road from start to end city
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Road (edge)</returns>
        Road FindTheRoad(City start, City end)
        {
            Road road = null;

            foreach (Road r in start.Roads)
            {
                if (r.DestinationCity == end)
                {
                    road = r;
                    break;
                }
            }
            return road;
        }

        /// <summary>
        /// Prints the cities in the given shortest path between two cities
        /// </summary>
        void PrintPath(RichTextBox output)
        {
            Separator(output);
            for (int i = 0; i < CityCount(); i++)
            {
                output.Text += TravelPath[i].Name;
                if (i != TravelPath.Count - 1)
                    output.Text += " -> ";
            }
            Separator(output);
        }

        /// <summary>
        /// Prints the travel summary
        /// </summary>
        void CitiesOnPath(RichTextBox output)
        {
            for (int i = 0; i < CityCount() - 1; i++)
            {
                Road current = FindTheRoad(TravelPath[i], TravelPath[i + 1]);
                output.Text += Environment.NewLine + TravelPath[i].Name + " -> " + TravelPath[i + 1].Name + ": " + current.Distance + "km";
            }
            Separator(output);
        }
    }
}
