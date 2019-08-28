using System;

namespace TravelEurope
{
    public enum Franchise
    {
        Shell = 1,
        OMV = 2,
        Lukoil = 3,
        Slovnaft = 4,
        noFranchise = 5
    }
    /// <summary>
    /// Provides refuelling of the car
    /// </summary>
    public class FillingStation
    {
        #region Instance variables

        public double Price { get;  private set; }
        public readonly Franchise Franchise;

        #endregion

        public FillingStation()
        {
            Price = 0;
            Franchise = ChooseFranchise();
        }

        public FillingStation(Franchise franchise)
        {
            Price = 0;
            Franchise = franchise;
        }
        
        //randomly choose the Franchise
        public static Franchise ChooseFranchise()
        {
            Random rand = new Random();
            return (Franchise)rand.Next(1, 6);
        }

        /// <summary>
        /// Establish the price for fuel by the type of fuel you want to use
        /// </summary>
        /// <param name="city"></param>
        /// <returns>Price in the local currency</returns>
        public double SetPrice(City city)
        {
            Random rand = new Random();

            switch (Car.Instance.FuelType)
            {
                case Fuel.gas:
                    Price = rand.Next(12, 21) / (double)10 * SetExchangeRate(city.Country.Code);
                    break;
                case Fuel.diesel:
                    Price = rand.Next(10, 18) / (double)10 * SetExchangeRate(city.Country.Code);
                    break;
                case Fuel.electro:
                    Price = rand.Next(5, 15) / (double)10 * SetExchangeRate(city.Country.Code);
                    break;
            }
            return Price;
        }

        /// <summary>
        /// Computes the exchange rate according to €, assume card as payment method
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        double SetExchangeRate(string countryCode)
        {
            var factory = Activator.CreateInstance(Type.GetType($"{countryCode}ExchangeRateFactory")) as IExchangeRateFactory;
            return factory.SetRate();
        }

        public interface IExchangeRateFactory
        {
            double SetRate();
        }

        public class CZExchangeRateFactory : IExchangeRateFactory
        {
            public double SetRate()
            {
                return 26.02;
            }
        }

        public class HUExchangeRateFactory : IExchangeRateFactory
        {
            public double SetRate()
            {
                return 329.77;
            }
        }

        public class MDExchangeRateFactory : IExchangeRateFactory
        {
            public double SetRate()
            {
                return 19.77;
            }
        }

        public class PLExchangeRateFactory : IExchangeRateFactory
        {
            public double SetRate()
            {
                return 4.37;
            }
        }

        public class ROExchangeRateFactory : IExchangeRateFactory
        {
            public double SetRate()
            {
                return 4.66;
            }
        }

        public class EURExchangeRateFactory : IExchangeRateFactory
        {
            public double SetRate()
            {
                return 1;
            }
        }
    }
}
