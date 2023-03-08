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
    /// February 18, 2023
    /// </summary>
    public class BookingsController : Controller
    {
        private TravelExpertsContext _context { get; set; }
        /// <summary>
        /// travel experts constructor
        /// </summary>
        /// <param name="context"></param>
        public BookingsController(TravelExpertsContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets customer ID from the session and retrieves all bookings
        /// by that customer to sent to the view
        /// </summary>
        /// <returns>Bookings index view</returns>
        [Authorize(Roles = "RegisteredUser")]
        public ActionResult Index()
        {
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            List<Booking> bookings = BookingManager.GetCustomerBookings(_context, (int)customerID);
            return View(bookings);
        }

        /// <summary>
        /// Gets booking details for a specific booking
        /// and send the booking details to the view
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <returns>Booking details view</returns>
        [Authorize(Roles = "RegisteredUser")]
        public ActionResult Details(int id)
        {
            List<BookingDetail> details = BookingManager.GetBookingDetails(_context, id);
            return View(details);
        }

        /// <summary>
        /// Calculates the cost of all bookings by the customer
        /// </summary>
        /// <returns>Booking detail list with all of the costs, 
        /// the total cost, total base price, and total commission</returns>
        public ActionResult CostBreakdown()
        {
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            decimal? basePrice = 0;
            decimal? commission = 0;
            decimal? totalCost = 0;
            List<Booking> details = BookingManager.GetCustomerBookings(_context, (int)customerID);
            List<BookingDetail> costList = new List<BookingDetail>();
            foreach(var booking in details)
            {
                List<BookingDetail> bookingDetail = BookingManager.GetBookingDetails(_context, booking.BookingId);
                foreach(var detail in bookingDetail)
                {
                    totalCost += detail.BasePrice;
                    basePrice += detail.BasePrice;
                    totalCost += detail.AgencyCommission;
                    commission += detail.AgencyCommission;
                    costList.Add(detail);
                }
            }
            ViewBag.TotalCost = totalCost;
            ViewBag.BasePrice = basePrice;
            ViewBag.Commission = commission;
            return View(costList);
        }

        /// <summary>
        /// Delete page for the selected booking to delete
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <returns>A delete view for the booking</returns>
        public ActionResult Delete(int id)
        {
            Booking booking = BookingManager.GetBooking(_context, id);
            return View(booking);
        }

        /// <summary>
        /// Post delete request
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <param name="booking">Booking that is trying to be deleted</param>
        /// <returns>Redirection to customer booking page if successful</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Booking booking)
        {
            try
            {
                int? detailID = BookingManager.GetBookingDetailID(_context, id);
                BookingManager.DeleteBookingDetails(_context, detailID);
                BookingManager.DeleteBooking(_context, id);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(booking);
            }
        }
    }
}
