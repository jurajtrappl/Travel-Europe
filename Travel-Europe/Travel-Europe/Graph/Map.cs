using System.Collections.Generic;

namespace TravelEurope
{
    /// <summary>
    /// Represents an undirected graph
    ///     Nodes are Cities, Edges are Roads between them
    /// </summary>
    public class Map
    {
        #region Instance variables

        public int Width;
        public int Height;
        public readonly Dictionary<string, Country> Countries;
        public readonly Dictionary<string, City> Cities;

        #endregion

        public Map()
        {
            Countries = new Dictionary<string, Country>();
            Cities = new Dictionary<string, City>();
        }

        public Map(int width, int height) : this()
        {
            Width = width;
            Height = height;
        }
        
        public Country GetCountry(string countryCode) => Countries[countryCode];

        public City GetCity(string name) => Cities[name];

        /// <summary>
        /// Adds the given country to the list of all Countries only if it's not already there
        /// Otherwise returns the country itself
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public Country GetOrAddCountry(Country country)
        {
            string countryCode = country.Code;

            if (Countries.ContainsKey(countryCode))
                return Countries[countryCode];
            else
            {
                Countries.Add(countryCode, country);
                return Countries[countryCode];
            }
        }

        /// <summary>
        /// Adds the given city to the list of all Cities only if it's not already there
        /// Otherwise returns the city itself
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public City GetOrAddCity(City city)
        {
            string name = city.Name;

            if (Cities.ContainsKey(name))
                return Cities[name];
            else
            {
                Cities.Add(name, city);
                return Cities[name];
            }
        }
        
        /// <summary>
        /// Computes the total distance between the Cities in the given list
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int DistanceToTravel(List<City> path)
        {
            int tDistance = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                Road current = FindTheRoad(path[i], path[i + 1]);
                tDistance += current.Distance;
            }

            return tDistance;
        }

        /// <summary>
        /// Computes the total time spent by travelling on the path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public double TotalDuration(List<City> path)
        {
            double duration = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                Road curr = FindTheRoad(path[i], path[i + 1]);

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
        /// Computes the time spent on the road between the given cities
        /// </summary>
        /// <param name="startCity"></param>
        /// <param name="destinationCity"></param>
        /// <returns></returns>
        public static double Duration(City startCity, City destinationCity)
        {
            double duration;
            Road current = FindTheRoad(startCity, destinationCity);

            if (Car.Instance.MaxSpeed >= current.MaxAllowedSpeed)
                duration = current.Distance / (double)current.MaxAllowedSpeed;
            else
                duration = current.Distance / (double)Car.Instance.MaxSpeed;
            
            return duration;
        }

        /// <summary>
        /// Finds the road from start to end city
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Road (edge)</returns>
        public static Road FindTheRoad(City start, City end)
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

        
    }
}
