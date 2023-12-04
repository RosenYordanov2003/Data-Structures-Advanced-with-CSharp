using System;
using System.Drawing;
using System.Linq;

namespace Exam.Categorization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Categorizator categorization = new Categorizator();
            categorization.AddCategory(new Category("1", "Gosho", "I love Ceco"));
            categorization.AddCategory(new Category("2", "Ceco", "I love Gosho"));
            categorization.AddCategory(new Category("3", "Kalata", "I love Fornite"));
            categorization.AddCategory(new Category("4", "Pesho", "I love Kalata"));
            categorization.AddCategory(new Category("5", "Vesko", "I love Kati"));
            categorization.AssignParent("3", "2");
            categorization.AssignParent("2", "1");
            categorization.AssignParent("4", "2");
            categorization.AssignParent("5", "3");

            Console.WriteLine(string.Join(" ", categorization.GetHierarchy("3")));
        }
    }
}
