using _01.Two_Three;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<string>();

            string[] arr = { "F", "C", "G", "A", "B", "D", "E", "K", "I", "G", "H", "J", "K" };
            for (int i = 0; i < 13; i++)
            {
                tree.Insert(arr[i]);
            }
            Console.WriteLine(tree.ToString());
        }


       private class IntWrapper : IComparable<IntWrapper>
       {
            public IntWrapper(int value)
            {
                Value = value;
            }
            public int Value { get; set; }


            public int CompareTo([AllowNull] IntWrapper other)
            {
                if (this.Value > other.Value)
                {
                    return 1;
                }
                else if (this.Value == other.Value)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            public override string ToString()
            {
                return this.Value.ToString();
            }
       }
    }
}
