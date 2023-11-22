namespace Demo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HashTable;
    internal class Program
    {
        static void Main(string[] args)
        {
            HashTable<string, int> hashTable = new HashTable<string, int>(2);

            KeyValue<string, int>[] elements = {
                new KeyValue<string, int>("Peter", 1),
                new KeyValue<string, int>("Maria", 2),
                new KeyValue<string, int>("George", 3),
                new KeyValue<string, int>("Kiril", 4)
            };
            foreach (KeyValue<string, int> element in elements)
            {
                hashTable.Add(element.Key, element.Value);
            }

            int value = 0;
            bool isKeyValue = hashTable.TryGetValue("Peter1", out value);
            Console.WriteLine(value);
            Console.WriteLine(isKeyValue);
        }
    }
}
