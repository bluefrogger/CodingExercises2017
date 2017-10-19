using System;
using System.Collections.Generic;

namespace TreeSearcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Node MyTree = SampleTree();
            //Node Result = DepthFirstSearch(MyTree, "G");
            Node Result2 = BreadthFirstSearch(MyTree, "G");

            //Console.WriteLine(Result.Data);
            Console.WriteLine(Result2.Data);
            Console.ReadLine();
        }

        public static Node SampleTree()
        {
            Node Root = new Node("A", new List<Node>()
            {
                new Node("B", new List<Node>()
                {
                    new Node("D", null) , new Node("E", null), new Node("F", null)
                }),
                new Node("C", new List<Node>()
                {
                    new Node("G", new List<Node>()
                    {
                        new Node("H", null)
                    })
                })
            });

            return Root;
        }

        public static Node DepthFirstSearch(Node nodeToSearch, string dataToSearch)
        {
            Console.WriteLine(nodeToSearch.Data);

            if (nodeToSearch.Data == dataToSearch)
            {
                return nodeToSearch;
            }

            if (nodeToSearch.Children != null)
            {
                for (int i = 0; i < nodeToSearch.Children.Count; i++)
                {
                    Node Result = DepthFirstSearch(nodeToSearch.Children[i], dataToSearch);
                    if (Result != null && Result.Data == dataToSearch)
                        return Result;
                    
                }
            }

            return null;
        }

        public static Node BreadthFirstSearch(Node nodeToSearch, string dataToSearch)
        {
            Console.WriteLine(nodeToSearch.Data);

            Queue<Node> TreeQueue = new Queue<Node>();
            TreeQueue.Enqueue(nodeToSearch);

            while (TreeQueue.Count > 0)
            {
                nodeToSearch = TreeQueue.Dequeue();

                if (nodeToSearch.Data == dataToSearch)
                    return nodeToSearch;

                if (nodeToSearch.Children != null)
                {
                    for (int i = 0; i < nodeToSearch.Children.Count; i++)
                    {
                        Console.WriteLine(nodeToSearch.Children[i].Data);
                        TreeQueue.Enqueue(nodeToSearch.Children[i]);

                        if (nodeToSearch.Children[i].Data == dataToSearch)
                        {
                            return nodeToSearch.Children[i];
                        }
                    }
                }
            }

            return null;
        }
    }
}
