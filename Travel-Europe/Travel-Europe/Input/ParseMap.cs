using System;
using System.Text.RegularExpressions;

namespace TravelEurope
{
    class ParseMap
    {
        //regex for input files
        static readonly string Country = @"(?<CountryCode>[A-Z]*) (?<CountryName>[A-Za-z]*) (?<CurrencyCode>[A-Z]*)";
        static readonly string City = @"(?<Name>[A-Za-z]*) (?<CountryCode>[A-Z]*) (?<XCoord>\d+) (?<YCoord>\d+)";
        static readonly string Road = @"(?<StartCity>[A-Za-z]*) (?<DestinationCity>[A-Za-z]*) (?<MaxAllowedSpeed>\d+)";

        //global counter
        static int Counter = 0;

        //stuff for regex pattern matching
        static MatchCollection M;

        /// <summary>
        /// Parse the input text file "mapData.txt"
        /// </summary>
        /// <param name="inputLines">Input</param>
        /// <returns>Map created from the input textfile</returns>
        public static Map ParseInput(string[] inputLines)
        {
            //creates a new map
            Map map = new Map();

            //parsing countries
            int numberOfCountries = int.Parse(inputLines[Counter].Trim());
            Counter++;

            for (int i = 0; i < numberOfCountries; i++)
            {
                M = Regex.Matches(inputLines[Counter], Country);
                foreach (Match match in M)
                {
                    string countryCode = match.Groups["CountryCode"].Value;
                    string countryName = match.Groups["CountryName"].Value;
                    Currency currency = new Currency(match.Groups["CurrencyCode"].Value);

                    Country newCountry = new Country(countryName, countryCode, currency);
                    map.GetOrAddCountry(newCountry);
                }
                Counter++;
            }

            //parsing cities
            int numberOfCities = int.Parse(inputLines[Counter].Trim());
            Counter++;

            for (int i = 0; i < numberOfCities; i++)
            {
                M = Regex.Matches(inputLines[Counter], City);
                foreach (Match match in M)
                {
                    string cityName = match.Groups["Name"].Value;
                    string countryCode = match.Groups["CountryCode"].Value;
                    int x = Convert.ToInt32(match.Groups["XCoord"].Value);
                    int y = Convert.ToInt32(match.Groups["YCoord"].Value);

                    City newCity = new City(cityName, map.GetCountry(countryCode), x, y);
                    map.GetOrAddCity(newCity);

                    Country country = map.GetCountry(newCity.Country.Code);
                    country.AddCity(newCity);
                }
                Counter++;
            }

            //parsing roads
            int numberOfRoads = int.Parse(inputLines[Counter].Trim());
            Counter++;

            for (int i = 0; i < numberOfRoads; i++)
            {
                M = Regex.Matches(inputLines[Counter], Road);
                foreach (Match match in M)
                {
                    City startCity = map.GetCity(match.Groups["StartCity"].Value);
                    City destinationCity = map.GetCity(match.Groups["DestinationCity"].Value);
                    int maxAllowedSpeed = int.Parse(match.Groups["MaxAllowedSpeed"].Value);

                    Road newRoad = new Road(startCity, destinationCity, maxAllowedSpeed);
                    Road newRoadBackwards = new Road(destinationCity, startCity, maxAllowedSpeed);
                    startCity.Roads.Add(newRoad);
                    destinationCity.Roads.Add(newRoadBackwards);
                }
                Counter++;
            }

            return map;
        }
    }
}
