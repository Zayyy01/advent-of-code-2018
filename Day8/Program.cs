using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day8
{
    class Node
    {
        public Node()
        {
            ChildNodes = new List<Node>();
            MetaData = new List<int>();
        }
        public List<Node> ChildNodes {get; set; }
        public List<int> MetaData {get; set; }



    }

    class Program
    {
        private static Queue<int> data = new Queue<int>();

        static void HandleInputFile(string path)
        {
            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = Regex.Split(line, " ");
                    foreach (var nbr in split)
                    {
                        data.Enqueue(int.Parse(nbr));
                    }
                }
            }
        }

        static Node CreateNode()
        {
            var nodeChild = data.Dequeue();
            var nodeMetadata = data.Dequeue();
            var node = new Node();
            foreach (var item in Enumerable.Range(0, nodeChild))
            {
                node.ChildNodes.Add(CreateNode());
            }

            foreach (var item in Enumerable.Range(0, nodeMetadata))
            {
                node.MetaData.Add(data.Dequeue());
            }

            return node;
        }

        static int SumOfMetadatas(Node n)
        {
            int sumOfChildrenMetadata = 0;
            foreach (var c in n.ChildNodes)
            {
                sumOfChildrenMetadata += SumOfMetadatas(c);
            }
            return sumOfChildrenMetadata + n.MetaData.Sum();
        }

        static int ValueOfNode(Node n)
        {
            if(n.ChildNodes.Count == 0)
                return n.MetaData.Sum();

            int s = 0;
            foreach (var m in n.MetaData)
            {
                if(m > 0 && n.ChildNodes.Count >= m)
                    s += ValueOfNode(n.ChildNodes[m - 1]);
            }

            return s;
        }

        static void Main(string[] args)
        {
            HandleInputFile(args[0]);

            var n = CreateNode();
            Console.WriteLine(SumOfMetadatas(n));
            Console.WriteLine(ValueOfNode(n));
            Console.ReadKey();
        }
    }
}
