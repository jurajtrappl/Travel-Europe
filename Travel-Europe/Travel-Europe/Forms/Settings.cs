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
            if (!ValidateForm())
            {
                MessageBox.Show("Please enter the information.");
            }
            else
            {
                Hide();
                Form simulationForm = new TravelSimulation();
                simulationForm.ShowDialog();
                Close();
            }
        }

        #region Input handling

        private bool ValidateTankCapacity()
        {
            bool validateStatus = true;

            if(textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "Please enter a tank capacity.");
            }
            else
            {
                errorProvider1.SetError(textBox2, "");
                try
                {
                    bool validInput = double.TryParse(textBox2.Text, out double value);
                    if ((validInput && value < 0) || !validInput)
                    {
                        validateStatus = false;
                        errorProvider1.SetError(textBox2, "Invalid input - Tank Capacity");
                    }
                    else
                    {
                        Car.Instance.TankCapacity = value;
                    }
                }
                catch
                {
                    errorProvider1.SetError(textBox2, "Please enter a correct tank capacity.");
                }
            }
            
            return validateStatus;
        }

        private bool ValidateMaxSpeed()
        {
            bool validateStatus = true;

            if(textBox4.Text == "")
            {
                errorProvider2.SetError(textBox4, "Please enter a max speed.");
            }
            else
            {
                errorProvider2.SetError(textBox4, "");
                try
                {
                    bool validInput = int.TryParse(textBox4.Text, out int value);
                    if ((validInput && value < 0) || !validInput)
                    {
                        validateStatus = false;
                        errorProvider2.SetError(textBox4, "Invalid input - Max Speed");
                    }
                    else
                    {
                        Car.Instance.MaxSpeed = value;
                    }
                }
                catch
                {
                    errorProvider2.SetError(textBox4, "Please enter a correct max speed");
                }
            }

            return validateStatus;
        }

        private bool ValidateConsumption()
        {
            bool validateStatus = true;

            if(textBox5.Text == "")
            {
                errorProvider3.SetError(textBox5, "Please enter a consumption.");
            }
            else
            {
                errorProvider3.SetError(textBox5, "");
                try
                {
                    bool validInput = double.TryParse(textBox5.Text, out double value);
                    if ((validInput && value < 0) || !validInput)
                    {
                        validateStatus = false;
                        errorProvider3.SetError(textBox5, "Invalid input - Consumption");
                    }
                    else
                    {
                        Car.Instance.Consumption = value;
                    }
                }
                catch
                {
                    errorProvider3.SetError(textBox5, "Please enter a correct consumption.");
                }
            }
            
            return validateStatus;
        }

        #endregion

        private void textBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateTankCapacity();
        }

        private void textBox4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateMaxSpeed();
        }

        private void textBox5_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateConsumption();
        }

        private bool ValidateForm()
        {
            bool validTankCapacity = ValidateTankCapacity();
            bool validMaxSpeed = ValidateMaxSpeed();
            bool validConsumption = ValidateConsumption();

            if(validTankCapacity && validMaxSpeed && validConsumption)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
