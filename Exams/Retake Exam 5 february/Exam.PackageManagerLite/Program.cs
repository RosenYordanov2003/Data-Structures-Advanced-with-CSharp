using System;

namespace Exam.PackageManagerLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PackageManager packageManager = new PackageManager();

            packageManager.RegisterPackage(new Package("1", "Gosho", new DateTime(2022, 12, 14), "1.0"));
            packageManager.RegisterPackage(new Package("2", "Pesho", new DateTime(2022, 10, 19), "1.1"));
            packageManager.RegisterPackage(new Package("3", "Ceco", new DateTime(2022, 10, 19), "1.1"));
            packageManager.RegisterPackage(new Package("4", "Kalata", new DateTime(2022, 10, 19), "1.1"));
            packageManager.RegisterPackage(new Package("5", "Vesko", new DateTime(2022, 10, 19), "1.1"));


            Console.WriteLine(string.Join(" ", packageManager.GetIndependentPackages()));

            Console.WriteLine(26%7);
        }
    }
}
