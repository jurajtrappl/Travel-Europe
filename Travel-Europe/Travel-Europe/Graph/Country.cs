using System.Collections.Generic;

namespace TravelEurope
{
    /// <summary>
    /// Data class for structuring map
    /// </summary>
    public class Country
    {
        #region Instance variables

        public readonly string Name;
        public readonly string Code;
        public readonly Currency Currency;
        public readonly List<City> Cities;

        #endregion

        public Country(string name, string code, Currency currency)
        {
            Name = name;
            Code = code;
            Currency = currency;
            Cities = new List<City>();
        }

        public void AddCity(City city) => Cities.Add(city);
    }
}
