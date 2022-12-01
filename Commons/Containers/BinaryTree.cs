using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Commons.Containers
{
    public class BinaryTree<T>
    {
        public class Node
        {

            #region Properties
            public T Data { get; set; }
            public Node Parent { get; set; }
            public (Node Left, Node Right) Children { get; set; }

            public bool IsRoot => this.Parent == null;

            #endregion

            #region Constructors

            public Node(Node parent, T data)
            {
                this.Parent = parent;
                this.Data = data;
            }

            #endregion

            #region Methods

            public void AddChildren(T leftData, T rightData)
            {
                var left = new Node(this, leftData);
                var right = new Node(this, rightData);

                this.Children = (left, right);
            }

            public bool IsParentOf(Node node)
            {
                Node currentNode = node;

                while (!currentNode.IsRoot && currentNode != this)
                    currentNode = currentNode.Parent;

                return currentNode == this;
            }

            #endregion
        }

        #region Properties

        public Node Root { get; set; }

        #endregion

        public BinaryTree(T data)
        { }

        public BinaryTree(Node root)
        { }
    }
}
