using System;
using System.Windows.Forms;

namespace TravelEurope
{
    public partial class Settings : Form
    {
        Map map;

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
            //open the simulation form
            Hide();
            Form simulationForm = new TravelSimulation(map, map.Cities[comboBox1.Text], map.Cities[comboBox2.Text]);
            simulationForm.ShowDialog();
            Close();
        }

        private void LoadMap(object sender, EventArgs e)
        {
            DialogResult File = openFileDialog1.ShowDialog();
            string fileName;

            if(File == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;

                //CREATE A MAP
                //parse the input file and load data to the map
                string[] inputLines = System.IO.File.ReadAllLines(fileName);
                map = ParseMap.ParseInput(inputLines);

                //fill comboboxes for picking the cities with city names
                foreach(string cityName in map.Cities.Keys)
                {
                    comboBox1.Items.Add(cityName);
                    comboBox2.Items.Add(cityName);
                }
            }
        }

        #region Input handling

        //tank capacity
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            bool validInput = double.TryParse(textBox2.Text, out double value);
            if ((validInput && value < 0) || !validInput)
                MessageBox.Show("Invalid input","Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Car.Instance.TankCapacity = value;
        }

        //tank status
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            bool validInput = double.TryParse(textBox3.Text, out double value);
            if ((validInput && value < 0) || !validInput || value > Car.Instance.TankCapacity)
                MessageBox.Show("Invalid input", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Car.Instance.TankStatus = value;
        }

        //max speed
        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            bool validInput = int.TryParse(textBox4.Text, out int value);
            if ((validInput && value < 0) || !validInput)
                MessageBox.Show("Invalid input", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Car.Instance.MaxSpeed = value;
        }

        //consumption
        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            bool validInput = double.TryParse(textBox5.Text, out double value);
            if ((validInput && value < 0) || !validInput)
                MessageBox.Show("Invalid input", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Car.Instance.Consumption = value;
        }

        #endregion
    }
}
