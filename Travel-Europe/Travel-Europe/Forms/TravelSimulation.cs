using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TravelEurope
{
    public partial class TravelSimulation : Form
    {
        #region Instance variables
        
        List<City> path;
        bool yesClicked = false;

        #endregion

        public TravelSimulation(Map map, City startCity, City endCity)
        {
            InitializeComponent();
           
            textBox1.Text = "0";

            Bitmap mapDraw = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            Graphics g = Graphics.FromImage(mapDraw);
            //cities
            foreach (City c in map.Cities.Values)
            {
                c.Draw(g, Color.Black);
            }

            //find the path
            path = Path.GetPath(map, startCity, endCity);

            //show the path plan
            PrepareSimulation(map);

            //draw road between cities on the path
            DrawRoads(g, path);

            //DrawAllRoads(g, map);

            pictureBox1.Image = mapDraw;
        }

        void TravelSimulation_Load(object sender, EventArgs e)
        {
            //settings for rich text box (logs)
            richTextBox1.WordWrap = true;
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
            richTextBox1.Font = new Font("Sitka Banner", 15, FontStyle.Bold);
            richTextBox1.MaxLength = 50;
        }

        void DrawAllRoads(Graphics g, Map map)
        {
            foreach(City c in map.Cities.Values)
            {
                foreach(Road r in c.Roads)
                {
                    r.Draw(g, Color.IndianRed);
                }
            }
        }

        //Draw roads on the path
        void DrawRoads(Graphics g, List<City> path)
        {
            for(int i = 0; i < path.Count - 1; i++)
            {
                foreach(Road r in path[i].Roads)
                {
                    if(r.DestinationCity == path[i + 1])
                    {
                        r.Draw(g, Color.Red);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Prints the cities in the given shortest path between two cities
        /// </summary>
        /// <param name="path">Cities</param>
        void PrintPath(List<City> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                richTextBox1.Text += Environment.NewLine + path[i].Name;
                if (i != path.Count - 1)
                    richTextBox1.Text += " -> ";
            }
        }

        /// <summary>
        /// Prints the travel summary
        /// </summary>
        /// <param name="path"></param>
        void TravelPlan(List<City> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Road current = Map.FindTheRoad(path[i], path[i + 1]);
                richTextBox1.Text += Environment.NewLine + path[i].Name + " -> " + path[i + 1].Name + ": " + current.Distance + "km";
            }
        }

        //design additions
        string Line() => "----------------------------------------------------";
        void WordAnimation()
        {
            richTextBox1.Text += Environment.NewLine + Line();
            richTextBox1.Text += Environment.NewLine + "     ...Travelling...";
            richTextBox1.Text += Environment.NewLine + Line();
        }

        public void PrepareSimulation(Map map)
        {
            richTextBox1.Text += "Trip from " + path[0].Name.ToUpper() + " to " + path[path.Count-1].Name.ToUpper();
            richTextBox1.Text += Environment.NewLine + Line();

            //FIND THE SHORTEST PATH
            richTextBox1.Text += Environment.NewLine + "Cities to go through:";

            //SHOW THE PATH
            PrintPath(path);
            richTextBox1.Text += Environment.NewLine + Line();

            //CALCULATIONS
            //calculates the distance between the start and destination citiy
            richTextBox1.Text += Environment.NewLine + "Total distance to travel: " + map.DistanceToTravel(path) + "km";
            //calculates the duration of travelling
            richTextBox1.Text += Environment.NewLine + "Overall duration: " + map.TotalDuration(path) + "h";

            //travel summary
            TravelPlan(path);
            richTextBox1.Text += Environment.NewLine + Line();
        }

        void YesButton(object sender, EventArgs e) => yesClicked = true;

        /// <summary>
        /// Simulates the road travelling
        /// </summary>
        /// <param name="path"></param>
        void Simulation(List<City> path)
        {
            FillingStation fillingStation = new FillingStation();

            richTextBox1.Text += Environment.NewLine + "Do you wish to fill up the tank?   (Yes/No)";
            if(yesClicked)
            {
                Franchise Franchise = FillingStation.ChooseFranchise();
                fillingStation = new FillingStation(Franchise);
                fillingStation.SetPrice(path[0]);
                Car.FillUpTheTank(fillingStation, path[0], richTextBox1, textBox1);
                yesClicked = false;
            }

            double remaining = 0;
            double neededFuel = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                WordAnimation();
                Road current = Map.FindTheRoad(path[i], path[i + 1]);
                richTextBox1.Text += Environment.NewLine + "Current path: " + path[i].Name + " -> " + path[i + 1].Name + " distance " + current.Distance + " km";
                richTextBox1.Text += Environment.NewLine + "Duration: " + Map.Duration(path[i], path[i + 1]) + "h";

                if (path[i].Country != path[i + 1].Country)
                    richTextBox1.Text += Environment.NewLine + "You have crossed the state borders between " + path[i].Country.Code + " and " + path[i + 1].Country.Code;

                switch (Car.Instance.FuelType)
                {
                    case Fuel.gas:
                        neededFuel = ((current.Distance / (double)100) + 1) * Car.Instance.Consumption;
                        richTextBox1.Text += Environment.NewLine + "On the road we consume: " + neededFuel + "l";
                        remaining = (int)Car.Instance.TankStatus - neededFuel;
                        if (remaining <= 0)
                        {
                            double minimalfillAmount = neededFuel - (int)Car.Instance.TankStatus;
                            richTextBox1.Text += Environment.NewLine + "     ! NOT ENOUGH FUEL !";
                            richTextBox1.Text += Environment.NewLine + "The fuel will not last. We need to fill up atleast " + minimalfillAmount + "l";
                            fillingStation.SetPrice(path[i]);
                            Car.FillUpTheTank(fillingStation, path[i], richTextBox1, textBox1);
                        }
                        Car.Instance.TankStatus -= neededFuel;
                        richTextBox1.Text += Environment.NewLine + "After the road, the tank status will be " + Car.Instance.TankStatus + "l";
                        WordAnimation();
                        break;
                    case Fuel.diesel:
                        neededFuel = ((current.Distance / (double)100) + 1) * Car.Instance.Consumption;
                        richTextBox1.Text += Environment.NewLine + "On the road we consume: " + neededFuel + "l";
                        remaining = (int)Car.Instance.TankStatus - neededFuel;
                        if (remaining <= 0)
                        {
                            double minimalfillAmount = neededFuel - (int)Car.Instance.TankStatus;
                            richTextBox1.Text += Environment.NewLine + "     ! NOT ENOUGH FUEL !";
                            richTextBox1.Text += Environment.NewLine + "The fuel will not last. We need to fill up atleast " + minimalfillAmount + "l";
                            fillingStation.SetPrice(path[i]);
                            Car.FillUpTheTank(fillingStation, path[i], richTextBox1, textBox1);
                        }
                        Car.Instance.TankStatus -= neededFuel;
                        richTextBox1.Text += Environment.NewLine + "After the road, the tank status will be " + Car.Instance.TankStatus + "l";
                        WordAnimation();
                        break;
                    case Fuel.electro:
                        double neededKWh = ((current.Distance / (double)100) + 1) * Car.Instance.Consumption;
                        richTextBox1.Text += Environment.NewLine + "On the road we consume: " + neededKWh + "kWh";
                        remaining = (int)Car.Instance.TankStatus - neededKWh;
                        if (remaining <= 0)
                        {
                            double minimalfillAmount = neededKWh - (int)Car.Instance.TankStatus;
                            richTextBox1.Text += Environment.NewLine + "     ! NOT ENOUGH FUEL !";
                            richTextBox1.Text += Environment.NewLine + "The fuel will not last. We need to fill up atleast " + minimalfillAmount + "kWh";
                            fillingStation.SetPrice(path[i]);
                            Car.FillUpTheTank(fillingStation, path[i], richTextBox1, textBox1);
                        }
                        Car.Instance.TankStatus -= neededKWh;
                        richTextBox1.Text += Environment.NewLine + "After the road, the tank status will be " + Car.Instance.TankStatus + "kWh";
                        WordAnimation();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Simulation(object sender, EventArgs e)
        {
            //SIMULATION
            //start of simulation
            Simulation(path);
            richTextBox1.Text += Environment.NewLine + Line();
            richTextBox1.Text += Environment.NewLine + "You have sucessfully reached the destination!";
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
        }
    }
}
