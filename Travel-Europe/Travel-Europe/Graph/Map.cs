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

        public readonly Dictionary<string, Country> Countries;
        public readonly Dictionary<string, City> Cities;

        #endregion

        public Map()
        {
            Countries = new Dictionary<string, Country>();
            Cities = new Dictionary<string, City>();
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

    }
}