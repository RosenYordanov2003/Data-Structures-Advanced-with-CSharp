using System;
using AA_Tree;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AATree<int>();
            tree.Insert(5);
            tree.Insert(10);
            tree.Insert(6);

            tree.PostOrder((x) => Console.Write($"{x} "));
        }
    }
}
