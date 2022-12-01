namespace AdventOfCode.Commons.Containers
{
    public class Tree<T>
    {
        public class Node
        {
            #region Properties
            public T Data { get; set; }
            public Node Parent { get; set; }
            public List<Node> Children { get; set; }

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

            public Node AddChild(T data)
            {
                Node child = new Node(this, data);
                this.Children.Add(child);
                return child;
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

        public Node Root { get; set; }

        public Tree(T data)
        {
            this.Root = new Node(null, data);
        }

        public Tree(Node root)
        {
            this.Root = root;
        }
    }
}
