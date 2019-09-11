using System;
using System.Collections.Generic;

namespace TravelEurope
{
    /// <summary>
    /// Data structure
    /// Type: MinHeap
    /// </summary>
    class Heap
    {
        #region Instance variables

        public HeapNode[] Nodes;
        public Dictionary<string, HeapNode> Names;
        public int Count;

        #endregion
        
        public Heap(int capacity)
        {
            Nodes = new HeapNode[capacity];
            Count = 0;
            Names = new Dictionary<string, HeapNode>();
        }
        
        /// <summary>
        /// Add the given node to the heap
        /// </summary>
        /// <param name="node"></param>
        public void Insert(HeapNode node)
        {
            if (Count == Nodes.Length)
                throw new Exception("Heap capacity exceeded.");

            int position = Count++;
            Nodes[position] = node;
            node.Position = position;

            Names[node.City.Name] = node;

            BubbleUp(position);
        }

        /// <summary>
        /// Returns and deletes the root of the heap
        /// </summary>
        /// <returns>Minimum element</returns>
        public HeapNode ExtractMin()
        {
            HeapNode min = new HeapNode(Nodes[0].Key, Nodes[0].City);
            Swap(0, Count - 1);
            Count--;
            BubbleDown(0);
            return min;
        }

        /// <summary>
        /// Reduce position of the given node
        /// </summary>
        /// <param name="node">Node to decrease</param>
        public void DecreaseKey(HeapNode node)
        {
            int position = node.Position;
            while ((position > 0) && (Nodes[Parent(position)].Key > node.Key))
            {
                int originalParentPosition = Parent(position);
                Swap(originalParentPosition, position);
                position = originalParentPosition;
            }
        }

        /// <summary>
        /// Bubbling the element from the given position so the heap structure is not violated
        /// </summary>
        /// <param name="position"></param>
        public void BubbleUp(int position)
        {
            while (position > 0 && Nodes[Parent(position)].Key > Nodes[position].Key)
            {
                int originalParentPosition = Parent(position);
                Swap(position, originalParentPosition);
                position = originalParentPosition;
            }
        }

        /// <summary>
        /// Bubbling the element from the given position so the heap structure is not violated
        /// </summary>
        /// <param name="position"></param>
        void BubbleDown(int position)
        {
            int lChild = LeftChild(position);
            int rChild = RightChild(position);
            int largest = 0;

            largest = ((lChild < Count) && (Nodes[lChild].Key < Nodes[position].Key)) ? lChild : position;

            if ((rChild < Count) && (Nodes[rChild].Key < Nodes[largest].Key))
            {
                largest = rChild;
            }

            if (largest != position)
            {
                Swap(position, largest);
                BubbleDown(largest);
            }
        }

        #region Utilities
        /// <summary>
        /// Returns the parent index given by the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static int Parent(int position) => (position - 1) / 2;

        /// <summary>
        /// Returns the left child of the element given by the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static int LeftChild(int position) => 2 * position + 1;

        /// <summary>
        /// Returns the right child of the element given by the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static int RightChild(int position) => 2 * position + 2;

        /// <summary>
        /// Perform the swap of two heapNodes
        /// Swap elements in the heapArray, positions and keys
        /// </summary>
        /// <param name="firstPosition"></param>
        /// <param name="secondPosition"></param>
        void Swap(int firstPosition, int secondPosition)
        {
            HeapNode temp = Nodes[firstPosition];
            Nodes[firstPosition] = Nodes[secondPosition];
            Nodes[secondPosition] = temp;

            Nodes[firstPosition].Position = firstPosition;
            Nodes[secondPosition].Position = secondPosition;
        }

        /// <summary>
        /// Returns array index of the heapNode with given name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetHeapNodeIndex(string name)
        {
            for(int i = 0; i < Nodes.Length; i++)
            {
                if(Nodes[i].City.Name == Names[name].City.Name)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion Utilities
    }
}
