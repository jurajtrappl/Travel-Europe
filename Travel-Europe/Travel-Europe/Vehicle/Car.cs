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
            int actualFuel = 0;

            Currency currAtStart = new Currency(path[0].Country.Code);

            int distance = 0;
            for(int i = 0; i < path.Count - 1; i++)
            {

            }
        }
    }
}
