using _01.RedBlackTree;
using System;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            // Arrange
            RedBlackTree<int> rbt = new RedBlackTree<int>();

            rbt.Insert(10);
            rbt.Insert(5);
            rbt.Insert(3);
            rbt.Insert(1);
            rbt.Insert(4);
            rbt.Insert(8);
            rbt.Insert(9);
            rbt.Insert(37);
            rbt.Insert(39);
            rbt.Insert(45);

            rbt.DeleteMax();

            rbt.EachInOrder((x) => Console.Write($"{x} "));
        }
    }
}
