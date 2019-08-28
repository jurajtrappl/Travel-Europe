using System.Collections.Generic;

namespace TravelEurope
{
    class Dijkstra
    {
        /// <summary>
        /// Dijkstra's algorhitm, optimized by binary heap
        /// Finds the shortest distance between the given Cities
        /// </summary>
        /// <param name="startCity"></param>
        /// <param name="endCity"></param>
        /// <returns>List of all Cities between startCity and endCity (inclusive)</returns>
        public static List<City> ShortestPath(Map map, City start, City end)
        {
            Dictionary<City, City> previous = new Dictionary<City, City>();
            //Heap heap = new Heap(map.Cities.Count);
            Heap heap = new Heap(map.Cities.Count);
            List<City> path = new List<City>();

            //initialization
            foreach (City city in map.Cities.Values)
            {
                if (city != start)
                {
                    heap.Insert(new HeapNode(int.MaxValue, city));
                }
                else
                {
                    heap.Insert(new HeapNode(0, city));
                }
            }

            while (heap.Count > 0)
            {
                HeapNode min = heap.ExtractMin();

                //end reached
                if(min.City.Name == end.Name)
                {
                    while (previous.ContainsKey(min.City))
                    {
                        path.Add(min.City);
                        min.City = previous[min.City];
                    }
                    break;
                }

                foreach(Road curr in min.City.Roads)
                {
                    string endCityName = curr.DestinationCity.Name;
                    HeapNode endCity = heap.Nodes[heap.GetHeapNodeIndex(endCityName)];
                    
                    int newDistance = min.Key + curr.Distance;
                    if(newDistance < endCity.Key)
                    {
                        previous[endCity.City] = min.City;
                        endCity.Key = min.Key + curr.Distance;
                        heap.DecreaseKey(endCity);
                    }
                }
            }

            path.Add(start);
            path.Reverse();
            return path;
        }
    }
}
