using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TravelEurope
{
    public enum Fuel
    {
        none,
        gas,
        diesel,
        electro
    }

    /// <summary>
    /// Vehicle used for transportation
    /// Singleton pattern (not threadsafe)
    /// </summary>
    public sealed class Car
    {
        static Car instance;

        Car()
        {
            FuelType = Fuel.none;
            TankCapacity = 0;
            Consumption = 0;
            MaxSpeed = 0;
        }

        public static Car Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Car();
                }
                return instance;
            }
        }

        #region Instance variables

        public Fuel FuelType;
        public double TankCapacity;
        public double Consumption;
        public int MaxSpeed;

        #endregion

        /// <summary>
        /// Refuel the tank of car
        /// </summary>
        /// <param name="city"></param>
        public static void CalculateFuel(RichTextBox output, List<City> path)
        {
            double actualFuel = 0;

            Currency currAtStart = new Currency(path[0].Country.Code);

            for(int i = 0; i < path.Count - 1; i++)
            {
                City startCity = path[i];
                City endCity = path[i + 1];

                Road between = null;
                foreach(Road r in startCity.Roads)
                {
                    if(r.DestinationCity == endCity)
                    {
                        between = r;
                        break;
                    }
                }

                int distance = between.Distance;
                int maxSpeed = between.MaxAllowedSpeed;

                actualFuel += Instance.Consumption * (distance / (double)100);
            }

            if (Instance.FuelType == Fuel.electro)
                output.Text += "Total fuel: " + actualFuel + "kWh." + Environment.NewLine;
            else
                output.Text += "Total fuel: " + actualFuel + "l." + Environment.NewLine;

            double fuelCost = CalculateFuelCost(actualFuel, currAtStart);

            output.Text += "The total cost of fuel is " + fuelCost + "€." + Environment.NewLine; //mena
        }

        public static double CalculateFuelCost(double fuelAmount, Currency currency)
        {
            double price = CalculateFuelPrice();
            return fuelAmount * price;
        } 

        public static double CalculateFuelPrice()
        {
            //just for simplicity - 1,5€ is price for 1l or 1kWh
            return 1.5;
        }
    }
}
