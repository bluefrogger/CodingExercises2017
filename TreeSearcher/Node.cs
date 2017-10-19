using System.Collections.Generic;

namespace TreeSearcher
{
    public class Node
    {
        public string Data { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }

        //public bool IsRoot { get { return Parent == null; } }
        //public bool IsLeaf { get { return Children.Count == 0; } }
        //public int Level { get { return IsRoot ? 0 : Parent.Level + 1; } }
        //public bool Visited { get; set; }
        public Node()
        {
            Data = null;
            Children = null;
        }

        public Node(string data, List<Node> children)
        {
            Data = data;
            Children = children;
        }
    }
}
