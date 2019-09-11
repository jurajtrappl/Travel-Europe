using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace TravelEurope
{
    public partial class TravelSimulation : Form
    {
        //path stuff
        static Map map;
        City startCity;
        City destinationCity;
        Queue<City> cityBetween;
        //graphic stuff
        static Bitmap drawMap;
        static Graphics graphics;

        public TravelSimulation()
        {
            InitializeComponent();

            //CREATE A MAP
            //parse the input file and load data to the map
            string[] inputLines = File.ReadAllLines("inputData/mapData.txt");
            map = ParseMap.ParseInput(inputLines);

            //settings for output (logs)
            output.WordWrap = true;
            output.ReadOnly = true;
            output.ScrollBars = RichTextBoxScrollBars.Both;
            output.Font = new Font("Sitka Banner", 15, FontStyle.Bold);
            output.MaxLength = 50;

            //graphics
            drawMap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            graphics = Graphics.FromImage(drawMap);
            pictureBox1.Image = drawMap;

            startCity = null;
            destinationCity = null;
            cityBetween = new Queue<City>();
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
        /// After clicking on the "Simulation" button this function checks whether user
        /// has selected cities. If yes, then the path is visualized and overview is printed. If not, then
        /// it prints the problem in the rich text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Simulate(object sender, EventArgs e)
        {
            if(startCity == null || destinationCity == null)
            {
                output.Text += "Pick cities first." + Environment.NewLine;
            }
            else if (startCity != null && destinationCity == null)
            {
                output.Text += "You have picked only starting city." + Environment.NewLine;
            }
            else if (startCity == null && destinationCity != null)
            {
                startCity = destinationCity;
                destinationCity = null;
                output.Text += "You have picked only starting city." + Environment.NewLine;
            }
            else
            {
                ResetOutput();

                //find the new path
                Path path = new Path(map, startCity, destinationCity, cityBetween);

                //show the path plan
                path.PathSummary(output);

                //calculate the possible refueling
                Car.CalculateFuel(output, path.TravelPath);

                //draw road between cities on the path
                DrawRoads(graphics, path.TravelPath);

                pictureBox1.Image = drawMap;
            }
        }

        /// <summary>
        /// Adds city to queue of cities that must be visited during the path from a starting city to a destination city.
        /// For loop checks for every city if it was clicked or not.
        /// </summary>
        /// <param name="e"></param>
        private void AddCity(MouseEventArgs e)
        {
            foreach(var city in map.Cities.Values)
            {
                if (city.Contains(e.X, e.Y))
                {
                    cityBetween.Enqueue(city);

                    output.Text += "You have picked " + city.Name + " as city to go through." + Environment.NewLine;
                }
                    
            }
        }

        /// <summary>
        /// Takes care of whether the user clicked on the city after clicking on the picturebox.
        /// If so, it will treat some basic cases.
        /// </summary>
        /// <param name="e"></param>
        private void SelectCity(MouseEventArgs e)
        {
            foreach (var city in map.Cities.Values)
            {
                if (city.Contains(e.X, e.Y))
                {
                    //add cities

                    if (city == startCity)
                    {
                        if(cityBetween.Count > 0)
                        {
                            //it is possible to travel from start to start through some cities
                            destinationCity = city;
                        }
                        else
                        {
                            output.Text += "You have already picked this city as start city." + Environment.NewLine;
                        }
                    }
                    else if (city == destinationCity)
                    {
                        output.Text += "You have already picked this city as destination city." + Environment.NewLine;
                    }
                    else if (startCity == null)
                    {
                        startCity = city;
                        
                        output.Text += "You have picked " + city.Name + " as start city." + Environment.NewLine;
                    }
                    else if (destinationCity == null)
                    {
                        destinationCity = city;
                        
                        output.Text += "You have picked " + city.Name + " as destination city." + Environment.NewLine;
                    }
                    else
                    {
                        output.Text += "You have already picked cities. Click \" Simulate\" to show the path or \"Reset\" if you want to show another path." + Environment.NewLine;
                    }
                }
            }
        }

        /// <summary>
        /// Deselects any selected city.
        /// </summary>
        /// <param name="e"></param>
        private void DismissCity(MouseEventArgs e)
        {
            //delete the selected city
            foreach (var city in map.Cities.Values)
            {
                if (city.Contains(e.X, e.Y))
                {
                    if (city == startCity)
                    {
                        output.Text += "You have deselected the start city." + Environment.NewLine;
                        startCity = null;
                    }
                    else if (city == destinationCity)
                    {
                        output.Text += "You have deselected the destination city." + Environment.NewLine;
                        destinationCity = null;
                    }
                    else if (cityBetween.Contains(city))
                    {
                        output.Text += "You have deselected the city to go through" + Environment.NewLine;
                        cityBetween = new Queue<City>(cityBetween.Where(p => p != city));
                    }
                    else
                    {
                        output.Text += "This city is not start or destination city." + Environment.NewLine;
                    }
                }
            }
        }

        /// <summary>
        /// Main function that selects action based on MouseEvent + Keys.Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickCity(object sender, MouseEventArgs e)
        {
            var clicked = e.Button;

            switch(clicked)
            {
                case MouseButtons.Left:
                    if(ModifierKeys == Keys.Control)
                    {
                        AddCity(e);
                    }
                    else
                    {
                        SelectCity(e);
                    }
                    break;
                case MouseButtons.Right:
                    DismissCity(e);
                    break;
                default:
                    output.Text += "Wrong mouse button." + Environment.NewLine;
                    break;
            }

            RenderMap();
        }

        void RenderMap()
        {
            ResetMap();

            if (startCity != null)
            {
                startCity.Draw(graphics, Color.Black);
            }
            if (destinationCity != null)
            {
                destinationCity.Draw(graphics, Color.Black);
            }
            if (cityBetween.Count > 0)
            {
                foreach (City c in cityBetween)
                {
                    c.Draw(graphics, Color.Red);
                }
            }

            pictureBox1.Image = drawMap;
        }

        private void ResetOutput() => output.Text = "";

        private void ResetMap()
        {
            //bitmap
            graphics.Clear(Color.White);
            graphics.Clear(Color.Transparent);
            pictureBox1.BackgroundImage = Properties.Resources.rsz_map;
            pictureBox1.Refresh();
        }

        private void ResetForm(object sender, EventArgs e)
        {
            //path
            startCity = null;
            destinationCity = null;
            
            foreach(City c in cityBetween)
            {
                cityBetween.Dequeue();
            }

            ResetOutput();

            ResetMap();
        }

        private void ChangeCarSettings(object sender, EventArgs e)
        {
            Form carSettings = new Settings();
            carSettings.Show();
        }
    }
}
