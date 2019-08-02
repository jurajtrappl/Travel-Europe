using System;
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
            TankStatus = 0;
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
        public double TankStatus;
        public double Consumption;
        public int MaxSpeed;

        #endregion

        /// <summary>
        /// Refuel the tank of car
        /// </summary>
        /// <param name="city"></param>
        public static void FillUpTheTank(FillingStation fStation, City city, RichTextBox output, TextBox input)
        {
            Currency currency = city.Country.Currency;
            string fuelType = (Instance.FuelType == Fuel.electro) ? "kWh" : "l";

            output.Text += Environment.NewLine + "We are at " + fStation.Franchise + " in " + city.Name +
                ". The price is " + fStation.Price + currency.Code + "/" + fuelType;

            output.Text += Environment.NewLine + "Tank status is " + Car.Instance.TankStatus + fuelType +
                ". Tank capacity is " + Car.Instance.TankCapacity + fuelType + "How much do you want to fill? (int)";

            int fillAmount = int.Parse(input.Text);
            //while (fillAmount <= 0 || fillAmount > Car.Instance.TankCapacity - Car.Instance.TankStatus)
            //{
            //    if (fillAmount <= 0)
            //        output.Text += Environment.NewLine + "You have entered invalid value.";
            //    else
            //        output.Text += Environment.NewLine + "Tank capacity is not enough.";

            //    fillAmount = Convert.ToInt32(input.text);
            //}

            double paid = fillAmount * fStation.Price;
            output.Text += Environment.NewLine + "We have paid " + paid + currency.Code;
            Instance.TankStatus += fillAmount;

            output.Text += Environment.NewLine + "You have filled up " + fillAmount + fuelType + ", current tank status: " + Instance.TankStatus + fuelType;
        }
    }
}
