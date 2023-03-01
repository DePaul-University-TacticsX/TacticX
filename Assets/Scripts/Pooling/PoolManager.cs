using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCintron.Pooling
{
    public abstract class PoolManager
    {
        protected int totalNodes;
        protected int deltaGrow;
        protected int numReserved;
        protected int numActive;

        //Dictionary to quickly find a specific active node
        private Dictionary<int, NodeBase> active;
        //Stack so data can easily grow/shrink
        private Stack<NodeBase> reserve;

        public PoolManager(int initialSize = 5, int deltaGrow = 2)
        {
            this.deltaGrow = deltaGrow;
            totalNodes = 0;
            numReserved = 0;
            numActive = 0;
            active = new Dictionary<int, NodeBase>();
            reserve = new Stack<NodeBase>();

            PrivFillReservedPool(initialSize);
        }

        public NodeBase Add()
        {
            NodeBase pNodeBase = PrivAdd();            

            // copy to active
            active[pNodeBase.GetHashCode()] = pNodeBase;

            return pNodeBase;
        }

        public void Remove(NodeBase node)
        {
            active.Remove(node.GetHashCode());

            node.Wash();

            reserve.Push(node);

            numActive--;
            numReserved++;
        }

        private NodeBase PrivAdd()
        {
            // Are there any nodes on the Reserve list?
            if (reserve.Count == 0)
            {
                // refill the reserve list by the DeltaGrow
                PrivFillReservedPool(deltaGrow);
            }

            // Always take from the reserve list
            NodeBase node = reserve.Pop();
            
            // Wash it
            DerivedWash(node);

            // Update stats
            numActive++;
            numReserved--;

            return node;
        }

        private void PrivFillReservedPool(int count)
        {
            totalNodes += count;
            numReserved += count;
            
            // Preload the reserve
            for (int i = 0; i < count; i++)
            {
                NodeBase node = DerivedCreateNode();
                reserve.Push(node);
            }
        }

        abstract protected NodeBase DerivedCreateNode();
        abstract protected void DerivedWash(NodeBase node);
    }
}