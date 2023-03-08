using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TravelExpertsData.Managers;
using TravelExpertsData.Models;

namespace TravelExpertsUI.Controllers
{
    /// <summary>
    /// Author: Aaron Li
    /// </summary>
    public class PackagesController : Controller
    {
        private TravelExpertsContext _context { get; set; }

        public PackagesController(TravelExpertsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of all packages and sends it to the packages index view
        /// </summary>
        /// <returns>View of all packages</returns>
        public ActionResult Index()
        {
            List<Package> packages = PackagesManager.GetAllPackages(_context);
            return View(packages);
        }

        /// <summary>
        /// Gets package from database and sends it to the book package view
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <returns>Book package view</returns>
        [Authorize(Roles = "RegisteredUser")]
        public ActionResult BookPackage(int id)
        {
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            Package package = PackagesManager.GetPackage(_context, id);

            if (customerID != null)
            {
                TempData["PackageId"] = id;
                TempData["CustomerId"] = customerID;
            }
            return View(package);
        }

        /// <summary>
        /// Creates an entry in the booking table and the
        /// booking details table in the database for the
        /// customer booking the package
        /// </summary>
        /// <returns>redirection to customer bookings</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RegisteredUser")]
        public ActionResult BookPackage()
        {
            int customerID = (int)TempData["CustomerId"];
            int packageID = (int)TempData["PackageId"];

            Package package = PackagesManager.GetPackage(_context, packageID);

            Booking newBooking = new Booking
            {
                BookingDate = DateTime.Now,
                TravelerCount = 1,
                CustomerId = customerID,
                PackageId = packageID
            };
            int bookingID = BookingManager.AddBooking(_context, newBooking);

            BookingDetail newBookingDetail = new BookingDetail
            {
                TripStart = package.PkgStartDate,
                TripEnd = package.PkgEndDate,
                Description = package.PkgDesc,
                BasePrice = package.PkgBasePrice,
                AgencyCommission = package.PkgAgencyCommission,
                BookingId = bookingID,
                ProductSupplierId = 1
            };
            BookingManager.AddBookingDetails(_context, newBookingDetail);
            return RedirectToAction("Index", "Bookings");
        }
    }
}
