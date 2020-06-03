using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace OS_Lab4
{
    public class Node : ICloneable
    {
        private IComparable data;
        private Node left, right;
        private int heightRight, heightLeft;

        #region Constructors
        public Node() : this(null) { }
        public Node(IComparable data) : this(data, null, null) 
        {
            heightLeft = 0;
            heightRight = 0;
        }
        public Node(IComparable data, Node left, Node right)
        {
            this.data = data;
            this.left = left;
            this.right = right;
            this.heightLeft = 0;
            this.heightRight = 0;
        }
        #endregion

        #region Public Methods
        public object Clone()
        {
            Node clone = new Node();
            if (data is ICloneable)
                clone.Value = (IComparable)((ICloneable)data).Clone();
            else
                clone.Value = data;
            clone.HeightLeft = heightLeft;
            clone.HeightRight = heightRight;

            return clone;
        }
        #endregion

        #region Public Properties
        public IComparable Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public Node Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }
        public Node Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }
        public int HeightRight
        {
            get
            {
                return heightRight;
            }
            set
            {
                heightRight = value;
            }
        }
        public int HeightLeft
        {
            get
            {
                return heightLeft;
            }
            set
            {
                heightLeft = value;
            }
        }
        #endregion
    }

}
