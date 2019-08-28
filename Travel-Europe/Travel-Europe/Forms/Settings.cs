using System;
using System.Windows.Forms;

namespace TravelEurope
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //engine type
            comboBox3.Items.Add("gas");
            comboBox3.Items.Add("diesel");
            comboBox3.Items.Add("electro");
        }

        private Fuel FuelFromString(string fuelType)
        {
            switch (fuelType)
            {
                case "gas":
                    return Fuel.gas;
                case "diesel":
                    return Fuel.diesel;
                case "electro":
                    return Fuel.electro;
                default:
                    return Fuel.none;
            }
        }


        private void Continue(object sender, EventArgs e)
        {
            FormCollection existingForms = Application.OpenForms;

            if (existingForms.Count == 1)
            {
                //for the first time
                
                //validate input
                
                Form simulationForm = new TravelSimulation();
                simulationForm.ShowDialog();
                Close();
            } else
            {
                Close();
            }
        }

        #region Input handling

        //private bool TankCapacity()
        //{
        //    bool done;

        //    bool validInput = double.TryParse(textBox2.Text, out double value);
        //    if ((validInput && value < 0) || !validInput)
        //    {
        //        done = false;
        //        MessageBox.Show("Invalid input - Tank Capacity", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    else
        //    {
        //        Car.Instance.TankCapacity = value;
        //        done = true;
        //    }

        //    return done;
        //}
        
        //private bool MaxSpeed()
        //{
        //    bool validInput = int.TryParse(textBox4.Text, out int value);
        //    if ((validInput && value < 0) || !validInput)
        //    {
        //        MessageBox.Show("Invalid input - Max Speed", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    else
        //    {
        //        Car.Instance.MaxSpeed = value;
        //    }

        //}

        //private bool Consumption()
        //{
        //    bool validInput = double.TryParse(textBox5.Text, out double value);
        //    if ((validInput && value < 0) || !validInput)
        //    {
        //        MessageBox.Show("Invalid input - Consumption", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    else
        //    {
        //        Car.Instance.Consumption = value;
        //    }
        //}

        #endregion
    }
}
