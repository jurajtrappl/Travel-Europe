using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEurope
{
    /// <summary>
    /// Represents map graph nodes in heap
    ///     so each heapNode has the same name as the City in the graph
    /// </summary>
    class HeapNode
    {
        #region Instance variables

        //refers to position in the heapArray
        public int Position;
        //"distance" in the graph
        public int Key;
        public City City;

        #endregion

        public HeapNode(int key, City city)
        {
            Key = key;
            City = city;
        }
    }
}
