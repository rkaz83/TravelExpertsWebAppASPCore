using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

namespace TravelExpertsData.Managers
{
    /// <summary>
    /// Author: Aaron Li
    /// February 18, 2023
    /// </summary>
    public static class PackagesManager
    {
        /// <summary>
        /// Retrieves Travel Package by the Package ID
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="id">Package ID</param>
        /// <returns>Package object</returns>
        public static Package GetPackage(TravelExpertsContext db, int id)
        {
            Package package = db.Packages.Find(id);
            return package;
        }

        /// <summary>
        /// Retrieves a list of all Travel Packages
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <returns>List of package objects</returns>
        public static List<Package> GetAllPackages(TravelExpertsContext db)
        {
            List<Package> packages = db.Packages
                .OrderByDescending(p => p.PkgStartDate)
                .ThenByDescending(p => p.PkgEndDate)
                .Select(p => p)
                .ToList();
            return packages;
        }
    }
}
