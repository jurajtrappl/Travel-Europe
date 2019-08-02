using System.Collections.Generic;

namespace TravelEurope
{
    class Path
    {
        /// <summary>
        /// Dijkstra's algorhitm, optimized by binary heap
        /// Finds the shortest distance between the given Cities
        /// </summary>
        /// <param name="startCity"></param>
        /// <param name="endCity"></param>
        /// <returns>List of all Cities between startCity and endCity (inclusive)</returns>
        public static List<City> GetPath(Map map, City start, City end)
        {
            Dictionary<City, City> previous = new Dictionary<City, City>();
            Heap heap = new Heap(map.Cities.Count);
            List<City> path = new List<City>();

            //initialization
            foreach (City city in map.Cities.Values)
                heap.Insert((city != start) ? new HeapNode(int.MaxValue, city) : new HeapNode(0, city));

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
                    HeapNode endCity = heap.Nodes[heap.GetHeapNodeIndex(curr.DestinationCity.Name)];


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
