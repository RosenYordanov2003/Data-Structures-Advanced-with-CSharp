namespace Demo
{
    using AVLTree;
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            AVL<int> avl = new AVL<int>();

            avl.Insert(5);
            avl.Insert(3);
            avl.Insert(1);
            avl.Insert(4);
            avl.Insert(8);
            avl.Insert(9);

            // Act
            avl.Delete(5);

        
            avl.EachInOrder((x) => Console.Write($"{x} "));
        }
    }
}
