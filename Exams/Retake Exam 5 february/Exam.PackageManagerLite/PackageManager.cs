using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.PackageManagerLite
{
    public class PackageManager : IPackageManager
    {
        private Dictionary<string, Package> packages = new Dictionary<string, Package>();

        public void RegisterPackage(Package package)
        {
            if (packages.Values.Any(p => p.Name == package.Name && p.Version == package.Version))
            {
                throw new ArgumentException("Package with the same name and version already exists.");
            }

            packages[package.Id] = package;
        }

        public void RemovePackage(string packageId)
        {
            if (!packages.ContainsKey(packageId))
            {
                throw new ArgumentException("Package not found.");
            }

            Package packageToRemove = packages[packageId];

            foreach (Package package in packages.Values)
            {
                package.Dependencies.Remove(packageToRemove);
            }

            packages.Remove(packageId);
        }

        public void AddDependency(string packageId, string dependencyId)
        {
            if (!packages.ContainsKey(packageId) || !packages.ContainsKey(dependencyId))
            {
                throw new ArgumentException("One or both packages not found.");
            }

            Package package = packages[packageId];
            Package dependency = packages[dependencyId];

            package.Dependencies.Add(dependency);
        }

        public bool Contains(Package package)
        {
            return packages.ContainsKey(package.Id);
        }

        public int Count()
        {
            return packages.Count;
        }

        public IEnumerable<Package> GetDependants(Package package)
        {
            return packages.Values.Where(p => p.Dependencies.Contains(package));
        }

        public IEnumerable<Package> GetIndependentPackages()
        {
            return packages.Values.Where(p => p.Dependencies.Count == 0)
                .OrderByDescending(p => p.ReleaseDate)
                .ThenBy(p => p.Version);
        }

        public IEnumerable<Package> GetOrderedPackagesByReleaseDateThenByVersion()
        {
            return packages.Values
                .GroupBy(p => p.Name)
                .Select(g => g.OrderByDescending(p => p.Version).First())
                .OrderByDescending(p => p.ReleaseDate)
                .ThenBy(p => p.Version);
        }
    }
}